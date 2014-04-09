﻿using System.Collections.Generic;
using System.Linq;

namespace Wimicrogrid
{
    public class Household
    {
        private readonly Appliances _appliances;
        private readonly int _id;

        public Household(int communityHouseCount, IEnumerable<Appliance> appliances, ITime clock)
        {
            _id = ++communityHouseCount;
            _appliances = new Appliances(clock);

            foreach (var appliance in appliances)
            {
                _appliances.Add(appliance);
            }
        }

        public int Id
        {
            get { return _id; }
        }
        
        public double TotalUsage
        {
            get { return _appliances.Sum(appliance => appliance.TotalUsage); }
        }

        public Appliances Appliances
        {
            get { return _appliances; }
        }

        public IEnumerable<Appliance> MagicBasement
        {
            get { return _appliances.All; }
        }

        public void AddAppliance(Appliance appliance)
        {
            _appliances.Add(appliance);
        }

        public void RemoveAppliance(string id)
        {
            var applianceToRemove = _appliances.SingleOrDefault(appliance => appliance.Id == id);

            if(applianceToRemove == null) return;

            _appliances.Remove(applianceToRemove);
        }

        public void SwitchOnOff(string id)
        {
            var applianceSwitch = _appliances.SingleOrDefault(appliance => appliance.Id == id);

            if (applianceSwitch == null) return;

            if (applianceSwitch.On)
            {
                applianceSwitch.SwitchOff();
                return;
            }
            applianceSwitch.SwitchOn();
        }
    }
}