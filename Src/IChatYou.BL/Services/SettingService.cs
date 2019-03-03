namespace IChatYou.BL.Services
{
    using DAL.Repositories.Interfaces;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;

    public class SettingService : ISettingService
    {
        private readonly ISettingRepository settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            this.settingRepository = settingRepository;
            EnsureCreated();
        }

        private IEnumerable<PropertyInfo> GetSettingProperties() => GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        public void Load()
        {
            foreach (var propertyInfo in GetSettingProperties())
            {
                var setting = settingRepository.GetByName(propertyInfo.Name);
                object value = null;

                if (setting != null)
                {
                    value = setting.Value;
                }
                else if (propertyInfo.GetCustomAttributes(typeof(DefaultSettingValueAttribute)).Any())
                {
                    var attribute = propertyInfo.GetCustomAttributes(typeof(DefaultSettingValueAttribute))
                        .FirstOrDefault();

                    value = ((DefaultSettingValueAttribute)attribute)?.Value;
                }

                if (value != null)
                {
                    propertyInfo.SetValue(this, Convert.ChangeType(value, propertyInfo.PropertyType));
                }
            }
        }

        public void Save()
        {
            foreach (var propertyInfo in GetSettingProperties())
            {
                settingRepository.Set(propertyInfo.Name, Convert.ToString(propertyInfo.GetValue(this)));
            }

            settingRepository.SaveChanges();
        }

        public void EnsureCreated()
        {
            Load();
            Save();
        }
    }
}
