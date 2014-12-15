WorkflowExtractor
=================

WFE **extracts rules** from Windows Workflow rules files or assemblies and converts the rules into C# and VB.NET class files.  It can also generate Word documents using another Open-Source library **DocX**

Some of the features of WFE are:

- Extract WF Rules from .rules files
- Extract WF Rules from .dll or .exe files
- Convert rules into **C# or VB.NET** Code
- Create **Word** Document for rules code
- Run the entire process in **Async** or Sync mode
- Configurable **settings file** for Code generation and Document generation
- Configurable **template file** to write custom description for different WF Activity types.  This description will then be used in generating Word document

Apart from generating word document, you can also write your own Output Generators and hook them using Dependency Injection and IoC patterns in the code.

It is developed in C# and Windows Workflow APIs to extract rules.
