using System;
using System.Collections.Generic;

namespace UserMgmtApi.Base
{
    public abstract class SmartEnum<TEnum> where TEnum : SmartEnum<TEnum>
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        protected SmartEnum(int id, string name)
        {
            Id = id;
            Name = name;
        }

        private static readonly List<TEnum> Values = new List<TEnum>();

        public static IEnumerable<TEnum> GetAll() => Values;

        public static TEnum GetById(int id) => Values.Find(item => item.Id == id);

        public static TEnum GetByName(string name) => Values.Find(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        protected static TEnum Create(int id, string name, TEnum instance)
        {
            instance.Id = id;
            instance.Name = name;
            Values.Add(instance);
            return instance;
        }
    }
}
