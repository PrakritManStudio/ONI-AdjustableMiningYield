using PeterHan.PLib.Options;

namespace AdjustableMiningYield
{
    public enum DuplicantSightRange
    {
        [Option("35%")]
        V35 = 35,
        [Option("50% Default")]
        V50 = 50,
        [Option("55%")]
        V55 = 55,
        [Option("60%")]
        V60 = 60,
        [Option("75%")]
        V75 = 75,
        [Option("100% Full Miner Yield")]
        V100 = 100,
        [Option("125%")]
        V125 = 125,
        [Option("150%")]
        V150 = 150
    }

    public class AdjustableMiningYieldConfig
    {
        [Option("Miner Results",
            "Volume obtained from mining")]
        public DuplicantSightRange YieldValue { get; set; } = DuplicantSightRange.V100;
    }
}
