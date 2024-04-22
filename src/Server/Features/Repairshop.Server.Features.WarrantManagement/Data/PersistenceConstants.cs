namespace Repairshop.Server.Features.WarrantManagement.Data;
internal static class PersistenceConstants
{
    public static class Tables
    {
        public const string Procedures = nameof(Procedures);
        public const string Warrants = nameof(Warrants);
        public const string WarrantSteps = nameof(WarrantSteps);
        public const string WarrantStepTransitions = nameof(WarrantStepTransitions);
        public const string Technicians = nameof(Technicians);
        public const string WarrantTemplates = nameof(WarrantTemplates);
        public const string WarrantTemplateSteps = nameof(WarrantTemplateSteps);
    }
}
