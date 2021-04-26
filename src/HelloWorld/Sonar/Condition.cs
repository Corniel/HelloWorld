namespace HelloWorld.Sonar
{
public readonly struct Condition<TContext> where TContext : BaseContext
{
    public delegate bool Lamda(TContext context);

    private Lamda Lamba { get; }
    private Condition(Lamda lamda) => Lamba = lamda;

    public bool Invoke(TContext context) => !(Lamba is null) && Lamba(context);

    private Condition<TContext> Or(Condition<TContext> other)
    {
        var lambda = Lamba;
        return new Condition<TContext>((context) => lambda(context) || other.Lamba(context));
    }
    private Condition<TContext> And(Condition<TContext> other)
    {
        var lambda = Lamba;
        return new Condition<TContext>((context) => lambda(context) && other.Lamba(context));
    }
    private Condition<TContext> Not()
    {
        var lambda = Lamba;
        return new Condition<TContext>((context) => !lambda(context));
    }

    public static implicit operator Condition<TContext>(Lamda lamda) => new Condition<TContext>(lamda);
    public static Condition<TContext> operator |(Condition<TContext> l, Condition<TContext> r) => l.Or(r);
    public static Condition<TContext> operator &(Condition<TContext> l, Condition<TContext> r) => l.And(r);
    public static Condition<TContext> operator ~(Condition<TContext> l) => l.Not();
}
}
