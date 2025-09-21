using System.Reflection;

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
        public AbilityType Type { get; }
        public void Use();
    }

    public class FireStrike : IAbility
    {
        public AbilityType Type => AbilityType.FireStike;

        public void Use() => Console.WriteLine("Casting Fire Strike!");
    }

    public class Heal : IAbility
    {
        public AbilityType Type => AbilityType.Heal;
        public void Use() => Console.WriteLine("Casting Heal!");
    }

    public class Teleport : IAbility
    {
        public AbilityType Type => AbilityType.Teleport;
        public void Use() => Console.WriteLine("Teleporting!");
    }

    // Ability Factory responsible for creating new abilities.
    public static class AbilityFactory
    {

        private static Dictionary<AbilityType, Type> abilitiesByType;
        private static bool IsInitialized => abilitiesByType != null;

        private static void InitializeFactory() //* Calling this because there is no constructor on a static class because it is not instantiated
        {
            if (IsInitialized) return;

            var abilitiesTypes = Assembly.GetAssembly(typeof(IAbility)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && typeof(IAbility).IsAssignableFrom(myType));

            abilitiesByType = new Dictionary<AbilityType, Type>();

            foreach (var type in abilitiesTypes)
            {
                var tempEffect = Activator.CreateInstance(type) as IAbility;
                abilitiesByType.Add(tempEffect.Type, type);
            }
        }

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

        public static IAbility GetAbility(AbilityType abilityType)
        {
            InitializeFactory();
            if (abilitiesByType.ContainsKey(abilityType))
            {
                Type type = abilitiesByType[abilityType];
                var ability = Activator.CreateInstance(type) as IAbility;
                return ability;
            }

            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IAbility ability = AbilityFactory.GetAbility(AbilityType.FireStike);
            ability.Use();
        }
    }
}

