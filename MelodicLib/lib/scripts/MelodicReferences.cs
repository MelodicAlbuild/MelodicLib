using System;
using System.Collections.Generic;
using System.Text;

namespace MelodicLib.lib.scripts
{
    class MelodicReferences
    {
        public static TrainProduction.GroupInfo GetOrCreateTyping(FactoryType factoryType)
        {
            TrainProduction production = new TrainProduction();
            return production.GetOrCreate(factoryType);
        }
    }
}
