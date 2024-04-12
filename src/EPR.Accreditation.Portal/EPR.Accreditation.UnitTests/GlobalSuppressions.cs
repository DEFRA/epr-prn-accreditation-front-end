// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "Microsoft themselves use this standard")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1629:Documentation text should end with a period", Justification = "We're not writing English essays. If it's a brief setences then a lack of full stop should be acceptable")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Another standard Microsoft do not use")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1413:Use trailing comma in multi-line initializers", Justification = "This is just unnecessary")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "Just adding this to reduce warnings. May have to be addressed at a later date")]

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "<Pending>")]
