namespace AbilityFactory
{
    public enum AbilityType
    {
        FireStike,
        Heal,
        Teleport,
    }

    public interface IAbility
    {
        public string Name { get; }
        public void Use();
    }

    public class FireStrike : IAbility
    {
        public string Name => "fire_strike";

        public void Use()
        {
            Console.WriteLine("Casting Fire Strike!");
        }
    }

    public class Heal : IAbility
    {
        public string Name => "heal";

        public void Use()
        {
            Console.WriteLine("Casting Heal!");
        }
    }

    public class Teleport : IAbility
    {
        public string Name => "teleport";

        public void Use()
        {
            Console.WriteLine("Teleporting!");
        }
    }

    // Ability Factory responsible for creating new abilities.
    public static class AbilityFactory //! still breaks open closed, cause to extend new abilities this class must be modified
    {
        public static IAbility CreateAbility(AbilityType type)
        {
            switch (type)
            {
                case AbilityType.FireStike:
                    return new FireStrike();
                case AbilityType.Heal:
                    return new Heal();
                case AbilityType.Teleport:
                    return new Teleport();
                default:
                    throw new ArgumentException("Tipo de habilidade inválido!");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            IAbility fireSpell = AbilityFactory.CreateAbility(AbilityType.FireStike);
            fireSpell.Use();
        }
    }
}

