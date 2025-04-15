public enum StatusType
{
    Speed,
    Armor,
    AttackPower,
}


public interface IStatModifier
{
    StatusType Type { get; }
    float GetModifier();
}


