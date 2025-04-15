public enum StatusType
{
    Speed,
    Armor,
    AttackPower,
    Damage,
}


public interface IStatModifier
{
    StatusType Type { get; }
    float GetModifier();
}


