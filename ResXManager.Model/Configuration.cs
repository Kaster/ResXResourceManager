﻿namespace ResXManager.Model
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using JetBrains.Annotations;

    using ResXManager.Infrastructure;
    using ResXManager.Model.Properties;

    public enum DuplicateKeyHandling
    {
        [LocalizedDisplayName(StringResourceKey.DuplicateKeyHandling_Rename)]
        Rename,
        [LocalizedDisplayName(StringResourceKey.DuplicateKeyHandling_Fail)]
        Fail
    }

    public interface IConfiguration
    {
        bool SaveFilesImmediatelyUponChange { get; }

        bool RemoveEmptyEntries { get; }

        [NotNull]
        CultureInfo NeutralResourcesLanguage { get; }

        StringComparison? EffectiveResXSortingComparison { get; }

        DuplicateKeyHandling DuplicateKeyHandling { get; }

        [NotNull]
        ResourceTableEntryRules Rules { get; }
    }

    [SuppressMessage("ReSharper", "NotNullMemberIsNotInitialized", Justification = "value provided by AutoProperties")]
    public abstract class Configuration : ConfigurationBase, IConfiguration
    {
        protected Configuration([NotNull] ITracer tracer)
            : base(tracer)
        {
        }

        [NotNull, UsedImplicitly]
        [DefaultValue(CodeReferenceConfiguration.Default)]
        public CodeReferenceConfiguration CodeReferences { get; }

        [DefaultValue(@"Migrations\\\d{15}"), CanBeNull]
        public string FileExclusionFilter { get; set; }

        [DefaultValue(true)]
        public bool SaveFilesImmediatelyUponChange { get; set; }

        [DefaultValue(true)]
        public bool RemoveEmptyEntries { get; set; }

        [DefaultValue(false)]
        public bool SortFileContentOnSave { get; set; }

        [DefaultValue("en-US")]
        public CultureInfo NeutralResourcesLanguage { get; set; }

        [DefaultValue(StringComparison.OrdinalIgnoreCase)]
        public StringComparison ResXSortingComparison { get; set; }

        public StringComparison? EffectiveResXSortingComparison => SortFileContentOnSave ? ResXSortingComparison : (StringComparison?)null;

        [DefaultValue(false)]
        public bool ConfirmAddLanguageFile { get; set; }

        [DefaultValue(false)]
        public bool AutoCreateNewLanguageFiles { get; set; }

        [DefaultValue(false)]
        public bool PrefixTranslations { get; set; }

        [DefaultValue("#TODO#_")]
        [CanBeNull]
        public string TranslationPrefix { get; set; }

        [CanBeNull]
        public string EffectiveTranslationPrefix => PrefixTranslations ? TranslationPrefix : string.Empty;

        [DefaultValue(default(ExcelExportMode))]
        public ExcelExportMode ExcelExportMode { get; set; }

        [DefaultValue(default(DuplicateKeyHandling))]
        public DuplicateKeyHandling DuplicateKeyHandling { get; set; }

        [UsedImplicitly]
        [DefaultValue(ResourceTableEntryRules.Default)]
        public ResourceTableEntryRules Rules { get; }

        [DefaultValue(false)]
        public bool ShowPerformanceTraces { get; set; }
    }
}
