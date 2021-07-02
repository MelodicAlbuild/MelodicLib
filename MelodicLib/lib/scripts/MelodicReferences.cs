namespace MelodicLib.lib.scripts {
    public class MelodicReferences {
        public static TrainProduction.GroupInfo GetOrCreateTyping(FactoryType factoryType) {
            var production = new TrainProduction();
            return production.GetOrCreate(factoryType);
        }
    }
}