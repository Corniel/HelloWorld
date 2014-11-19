using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace HelloWorld.Drawing
{
	/// <summary>Represents a Windows multi icon, which is a collection of small bitmap images
	/// used to represent an object. Icons can be thought of as transparent
	/// bitmaps, although their size is determined by the system.
	/// </summary>
	public class MultiIcon : List<Icon>
	{
		/// <summary>Initializes a new instance of the Tjip.Drawing.MultiIcon class.</summary>
		public MultiIcon() { }

		/// <summary>Represents the Widows multi icon as System.String.</summary>
		public override string ToString()
		{
			var str = string.Format("{0} Items: {1}",
				GetType().FullName,
				this.Count);
			if (this.Count == 1)
			{
				str += string.Format(", Size: {0} px",
					this.First().Width);
			}
			if (this.Count > 1)
			{
				str += string.Format(", Smallest: {0} px, Largest: {1} px",
					this.GetSmallest().Width,
					this.GetLargest().Width);
			}
			return str;
		}

		/// <summary>Loads a Windows multi icon based on the specfied path.</summary>
		/// <param name="filepath">The filepath to load from.</param>
		public static MultiIcon Load(string filepath)
		{
			using (var stream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
			{
				return Load(stream);
			}
		}
		/// <summary>Loads a Windows multi icon from a stream.</summary>
		/// <param name="stream">The stream to load from.</param>
		public static MultiIcon Load(Stream stream)
		{
			// Read the stream.
			using (var reader = new BinaryReader(stream))
			{
				var icon = new MultiIcon();
				var entries = new List<MultiIconEntry>();

				// Read the header.
				var header = reader.ReadMultiIconHeader();

				// Read the icon entries.
				for (int i = 0; i < header.Count; i++)
				{
					var entry = reader.ReadMultiIconEntry();
					entries.Add(entry);
				}
				// Read the icons based on the entries.
				foreach (var entry in entries)
				{
					var ico = reader.ReadIcon(header, entry);
					icon.Add(ico);
				}
				return icon;
			}
		}
	}

	/// <summary>Methods for Windows multi icon, that should be available
	/// for all collections of Widows icons.</summary>
	public static class MultiIconExtensions
	{
		/// <summary>Saves the Windows icons to a single file.</summary>
		/// <param name="icon">The list of icons.</param>
		/// <param name="filepath">The filepath to save to.</param>
		public static void Save(this IList<Icon> icons, string filepath)
		{
			using (var stream = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write))
			{
				icons.Save(stream);
			}
		}
		/// <summary>Saves the Windows icons to a single stream.</summary>
		/// <param name="icon">The list of icons.</param>
		/// <param name="stream">The stream to save to.</param>
		public static void Save(this IList<Icon> icons, Stream stream)
		{
			using (var writer = new BinaryWriter(stream))
			{
				long startposition = stream.Position;

				var entries = new List<MultiIconEntry>();
				var buffers = new List<byte[]>();
				// create header.
				var header = new MultiIconHeader()
				{
					Count = (short)icons.Count,
				};
				// create entries.
				for (int i = 0; i < icons.Count; i++)
				{
					using (var icon_stream = new MemoryStream())
					{
						var entry = new MultiIconEntry();
						var item = icons[i];
						item.Save(icon_stream);
						icon_stream.Position = 0;

						using (var reader = new BinaryReader(icon_stream))
						{
							// icoHeader.Reserved[2],
							// icoHeader.Type[2],
							// none.Pos[2],
							// Width[1],
							// Height[1],
							// ColorCount[1],
							// Reserved[1]
							// Planes[2]
							// BitCount[2]
							// BytesInRes[4]
							// none.OffSet[4]
							var header_reserved = reader.ReadInt16();
							var header_type = reader.ReadInt16();
							var none_startpos = reader.ReadInt16();
							entry.Width = reader.ReadByte();
							entry.Height = reader.ReadByte();
							entry.ColorCount = reader.ReadByte();
							entry.Reserved = reader.ReadByte();
							entry.Planes = reader.ReadInt16();
							entry.BitCount = reader.ReadInt16();
							entry.BytesInRes = reader.ReadInt32();
							entry.ImageOffset = MultiIconHeader.ByteSize + MultiIconEntry.ByteSize * icons.Count + buffers.Sum(buf => buf.Length);
							var none_offset = reader.ReadInt32();

							var buffer = new byte[icon_stream.Length - icon_stream.Position];
							icon_stream.Read(buffer, 0, buffer.Length);
							entries.Add(entry);
							buffers.Add(buffer);
						}
					}
				}
				// Writer header.
				writer.Write(header);

				// Write entries.
				foreach (var entry in entries)
				{
					writer.Write(entry);
				}
				// Write images.
				foreach (var buffer in buffers)
				{
					writer.Write(buffer);
				}
				// Clear buffer and save.
				writer.Flush();
			}
		}

		/// <summary>Gets the smallest icon.</summary>
		/// <param name="icon">The list of icons.</param>
		/// <returns>The smallest icon.</returns>
		public static Icon GetSmallest(this IEnumerable<Icon> icons)
		{
			var icon =
			(
				from
					item in icons
				orderby
					item.Width ascending
				select
					item
			)
			.FirstOrDefault();

			return icon;
		}
		/// <summary>Gets the largest icon.</summary>
		/// <param name="icon">The list of icons.</param>
		/// <returns>The largest icon.</returns>
		public static Icon GetLargest(this IEnumerable<Icon> icons)
		{
			var icon =
			(
				from
					item in icons
				orderby
					item.Width descending
				select
					item
			)
			.FirstOrDefault();

			return icon;
		}
	}
	/// <summary>Represents the header of a Windows (multi) icon.</summary>
	internal class MultiIconHeader
	{
		/// <summary>The byte size of a single icon header.</summary>
		public const int ByteSize = 2 + 2 + 2;

		/// <summary>Initializes a new instance of the Tjip.Drawing.MultiIconHeader class.</summary>
		public MultiIconHeader()
		{
			this.Type = 1;
		}

		public short Reserved { get; set; }
		public short Type { get; set; }
		public short Count { get; set; }
	}

	/// <summary>Extension methods for MultiIconHeader.</summary>
	internal static class MultiIconHeaderExtensions
	{
		/// <summary>Reads a Windows (multi) icon header from the current stream
		/// and advances the current position of the stream by six bytes.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public static MultiIconHeader ReadMultiIconHeader(this BinaryReader reader)
		{
			var header = new MultiIconHeader()
			{
				Reserved = reader.ReadInt16(),
				Type = reader.ReadInt16(),
				Count = reader.ReadInt16(),
			};
			return header;
		}

		/// <summary>Writes a Windows (multi) icon header to the current stream
		/// and advances the current position of the stream by six bytes.
		/// </summary>
		/// <param name="writer">The writer.</param>
		/// <param name="header">The Windows multi icon header to write.</param>
		public static void Write(this BinaryWriter writer, MultiIconHeader header)
		{
			writer.Write(header.Reserved);
			writer.Write(header.Type);
			writer.Write(header.Count);
		}
	}
	/// <summary>Represents the entry of a Windows (multi) icon.</summary>
	internal class MultiIconEntry
	{
		/// <summary>The byte size of a single icon entry.</summary>
		public const int ByteSize = 1 + 1 + 1 + 1 + 2 + 2 + 4 + 4;

		/// <summary>Initializes a new instance of the Tjip.Drawing.MultiIconEntry class.</summary>
		public MultiIconEntry() { }

		public byte Width { get; set; }
		public byte Height { get; set; }
		public byte ColorCount { get; set; }
		public byte Reserved { get; set; }
		public short Planes { get; set; }
		public short BitCount { get; set; }
		public int BytesInRes { get; set; }
		public int ImageOffset { get; set; }
	}

	/// <summary>Extension methods for MultiIconEntry.</summary>
	internal static class MultiIconEntryrExtensions
	{
		/// <summary>Reads a Windows (multi) icon entry from the current stream
		/// and advances the current position of the stream by sixteen bytes.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public static MultiIconEntry ReadMultiIconEntry(this BinaryReader reader)
		{
			var entry = new MultiIconEntry()
			{
				Width = reader.ReadByte(),
				Height = reader.ReadByte(),
				ColorCount = reader.ReadByte(),
				Reserved = reader.ReadByte(),
				Planes = reader.ReadInt16(),
				BitCount = reader.ReadInt16(),
				BytesInRes = reader.ReadInt32(),
				ImageOffset = reader.ReadInt32(),
			};
			return entry;
		}

		/// <summary>Reads a Windows icon from the current stream
		/// and advances the current position of the stream to the end of the
		/// Windows icon in the stream.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public static Icon ReadIcon(this BinaryReader reader, MultiIconHeader header, MultiIconEntry entry)
		{
			const short ICON_STREAM_START = 1;
			const int ICON_STREAM_OFFSET = 22;

			using (var newIcon = new MemoryStream())
			{
				using (var writer = new BinaryWriter(newIcon))
				{
					// Write it
					writer.Write(header.Reserved);
					writer.Write(header.Type);
					writer.Write(ICON_STREAM_START);
					writer.Write(entry.Width);
					writer.Write(entry.Height);
					writer.Write(entry.ColorCount);
					writer.Write(entry.Reserved);
					writer.Write(entry.Planes);
					writer.Write(entry.BitCount);
					writer.Write(entry.BytesInRes);
					writer.Write(ICON_STREAM_OFFSET);

					// Grab the icon
					byte[] tmpBuffer = new byte[entry.BytesInRes];
					reader.BaseStream.Position = entry.ImageOffset;
					reader.Read(tmpBuffer, 0, entry.BytesInRes);
					writer.Write(tmpBuffer);

					// Finish up
					writer.Flush();
					newIcon.Position = 0;
					return new Icon(newIcon);
				}
			}
		}

		/// <summary>Writes a Windows (multi) icon entry to the current stream
		/// and advances the current position of the stream by sixteen bytes.
		/// </summary>
		/// <param name="writer">The writer.</param>
		/// <param name="entry">The Windows multi icon entry to write.</param>
		public static void Write(this BinaryWriter writer, MultiIconEntry entry)
		{
			writer.Write(entry.Width);
			writer.Write(entry.Height);
			writer.Write(entry.ColorCount);
			writer.Write(entry.Reserved);
			writer.Write(entry.Planes);
			writer.Write(entry.BitCount);
			writer.Write(entry.BytesInRes);
			writer.Write(entry.ImageOffset);
		}
	}
}