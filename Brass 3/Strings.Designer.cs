﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.1433
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Brass3 {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Brass3.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Running in interactive calculator mode. Type exit to quit..
        /// </summary>
        internal static string CommandLineCalculatorMode {
            get {
                return ResourceManager.GetString("CommandLineCalculatorMode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error in {1} line {2} column {3}: {0}.
        /// </summary>
        internal static string CommandLineError {
            get {
                return ResourceManager.GetString("CommandLineError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fatal Error..
        /// </summary>
        internal static string CommandLineFatalError {
            get {
                return ResourceManager.GetString("CommandLineFatalError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usage: Brass ProjectFile.
        /// </summary>
        internal static string CommandLineSyntax {
            get {
                return ResourceManager.GetString("CommandLineSyntax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Warning in {1} line {2} column {3}: {0}.
        /// </summary>
        internal static string CommandLineWarning {
            get {
                return ResourceManager.GetString("CommandLineWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected {0} argument(s)..
        /// </summary>
        internal static string ErrorArgumentCountMismatch {
            get {
                return ResourceManager.GetString("ErrorArgumentCountMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected {0} or more arguments..
        /// </summary>
        internal static string ErrorArgumentCountMismatchEndlessRange {
            get {
                return ResourceManager.GetString("ErrorArgumentCountMismatchEndlessRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected {0} to {1} argument(s)..
        /// </summary>
        internal static string ErrorArgumentCountMismatchRange {
            get {
                return ResourceManager.GetString("ErrorArgumentCountMismatchRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Argument {0} must be positive..
        /// </summary>
        internal static string ErrorArgumentExpectedPositive {
            get {
                return ResourceManager.GetString("ErrorArgumentExpectedPositive", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected a single token value for argument {0}..
        /// </summary>
        internal static string ErrorArgumentExpectedSingleToken {
            get {
                return ResourceManager.GetString("ErrorArgumentExpectedSingleToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t extract a single token and index..
        /// </summary>
        internal static string ErrorArgumentExpectedSingleTokenAndIndex {
            get {
                return ResourceManager.GetString("ErrorArgumentExpectedSingleTokenAndIndex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Argument types cannot have mandatory arguments following optional arguments.
        /// </summary>
        internal static string ErrorArgumentOptionalNotLast {
            get {
                return ResourceManager.GetString("ErrorArgumentOptionalNotLast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to RepeatForever can only apply to the last argument..
        /// </summary>
        internal static string ErrorArgumentRepeatForeverNotLast {
            get {
                return ResourceManager.GetString("ErrorArgumentRepeatForeverNotLast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Argument type {0} not supported..
        /// </summary>
        internal static string ErrorArgumentUnsupportedType {
            get {
                return ResourceManager.GetString("ErrorArgumentUnsupportedType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No assembler set..
        /// </summary>
        internal static string ErrorAssemblerNotSet {
            get {
                return ResourceManager.GetString("ErrorAssemblerNotSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Assembler not explicitly set, so assuming {0}..
        /// </summary>
        internal static string ErrorAssemblerNotSetAssumeDefault {
            get {
                return ResourceManager.GetString("ErrorAssemblerNotSetAssumeDefault", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Automatically wrapped encodings must be single-byte only..
        /// </summary>
        internal static string ErrorAutomaticStringEncoderIsMultibyte {
            get {
                return ResourceManager.GetString("ErrorAutomaticStringEncoderIsMultibyte", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Must be base 2, 8 or 16..
        /// </summary>
        internal static string ErrorBaseMustByTwoEightSixteen {
            get {
                return ResourceManager.GetString("ErrorBaseMustByTwoEightSixteen", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This token isn&apos;t an open or close bracket..
        /// </summary>
        internal static string ErrorBracketExpected {
            get {
                return ResourceManager.GetString("ErrorBracketExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Brackets don&apos;t match..
        /// </summary>
        internal static string ErrorBracketMismatch {
            get {
                return ResourceManager.GetString("ErrorBracketMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Matching closing bracket not found..
        /// </summary>
        internal static string ErrorBracketNotClosed {
            get {
                return ResourceManager.GetString("ErrorBracketNotClosed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid starting bracket index..
        /// </summary>
        internal static string ErrorBracketStartIndexOutOfBounds {
            get {
                return ResourceManager.GetString("ErrorBracketStartIndexOutOfBounds", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} error(s) found: Cancelling build..
        /// </summary>
        internal static string ErrorCancellingBuild {
            get {
                return ResourceManager.GetString("ErrorCancellingBuild", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Compile process follows a different path in different passes. This is likely due to a bug in a plugin causing unintentional side-effects..
        /// </summary>
        internal static string ErrorCompilerFollowsDifferentPath {
            get {
                return ResourceManager.GetString("ErrorCompilerFollowsDifferentPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid directive &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorDirectiveNotDeclared {
            get {
                return ResourceManager.GetString("ErrorDirectiveNotDeclared", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dynamic data can only be written during the main compilation pass..
        /// </summary>
        internal static string ErrorDynamicDataCannotBeWrittenOutsideMainPass {
            get {
                return ResourceManager.GetString("ErrorDynamicDataCannotBeWrittenOutsideMainPass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Environment variable Brass.Templates not set..
        /// </summary>
        internal static string ErrorEnvironmentNotSetTemplates {
            get {
                return ResourceManager.GetString("ErrorEnvironmentNotSetTemplates", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No label found for label access operator..
        /// </summary>
        internal static string ErrorEvaluationAccessorLabelNotFound {
            get {
                return ResourceManager.GetString("ErrorEvaluationAccessorLabelNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot perform assignments in this expression..
        /// </summary>
        internal static string ErrorEvaluationAssignmentsNotPermitted {
            get {
                return ResourceManager.GetString("ErrorEvaluationAssignmentsNotPermitted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected field access..
        /// </summary>
        internal static string ErrorEvaluationExpectedFieldAccess {
            get {
                return ResourceManager.GetString("ErrorEvaluationExpectedFieldAccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected operand after operator..
        /// </summary>
        internal static string ErrorEvaluationExpectedOperandAfterOperator {
            get {
                return ResourceManager.GetString("ErrorEvaluationExpectedOperandAfterOperator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected operand before operator..
        /// </summary>
        internal static string ErrorEvaluationExpectedOperandBeforeOperator {
            get {
                return ResourceManager.GetString("ErrorEvaluationExpectedOperandBeforeOperator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Function &apos;{0}&apos; not declared..
        /// </summary>
        internal static string ErrorEvaluationFunctionNotDeclared {
            get {
                return ResourceManager.GetString("ErrorEvaluationFunctionNotDeclared", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing matching conditional operator &apos;?&apos;..
        /// </summary>
        internal static string ErrorEvaluationMissingConditionalOperator {
            get {
                return ResourceManager.GetString("ErrorEvaluationMissingConditionalOperator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Syntax error..
        /// </summary>
        internal static string ErrorEvaluationNoSingleResult {
            get {
                return ResourceManager.GetString("ErrorEvaluationNoSingleResult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nothing to evaluate..
        /// </summary>
        internal static string ErrorEvaluationNothingToEvaluate {
            get {
                return ResourceManager.GetString("ErrorEvaluationNothingToEvaluate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} operands unsupported..
        /// </summary>
        internal static string ErrorEvaluationOperandsUnsupported {
            get {
                return ResourceManager.GetString("ErrorEvaluationOperandsUnsupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t get type information..
        /// </summary>
        internal static string ErrorEvaluationTypeInformationMissing {
            get {
                return ResourceManager.GetString("ErrorEvaluationTypeInformationMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An assignment must be made..
        /// </summary>
        internal static string ErrorExpectedAssignment {
            get {
                return ResourceManager.GetString("ErrorExpectedAssignment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expected a single token..
        /// </summary>
        internal static string ErrorExpectedSingleToken {
            get {
                return ResourceManager.GetString("ErrorExpectedSingleToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field &apos;{0}&apos; not defined..
        /// </summary>
        internal static string ErrorFieldNotDeclared {
            get {
                return ResourceManager.GetString("ErrorFieldNotDeclared", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Flow control has been temporarily disabled..
        /// </summary>
        internal static string ErrorFlowControlDisabled {
            get {
                return ResourceManager.GetString("ErrorFlowControlDisabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Duplicate label &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorLabelAlreadyDefined {
            get {
                return ResourceManager.GetString("ErrorLabelAlreadyDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not parse &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorLabelCannotParse {
            get {
                return ResourceManager.GetString("ErrorLabelCannotParse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot remove the predefined output counter label..
        /// </summary>
        internal static string ErrorLabelCannotRemoveOutputCounter {
            get {
                return ResourceManager.GetString("ErrorLabelCannotRemoveOutputCounter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You cannot remove the predefined program counter label..
        /// </summary>
        internal static string ErrorLabelCannotRemoveProgramCounter {
            get {
                return ResourceManager.GetString("ErrorLabelCannotRemoveProgramCounter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid name &apos;{0}&apos; (looks like a number)..
        /// </summary>
        internal static string ErrorLabelInvalidNameNumber {
            get {
                return ResourceManager.GetString("ErrorLabelInvalidNameNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Label &apos;{0}&apos; is constant and cannot be assigned to..
        /// </summary>
        internal static string ErrorLabelIsConstant {
            get {
                return ResourceManager.GetString("ErrorLabelIsConstant", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Label &apos;{0}&apos; not found..
        /// </summary>
        internal static string ErrorLabelNotFound {
            get {
                return ResourceManager.GetString("ErrorLabelNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Page value for label &apos;{0}&apos; not found..
        /// </summary>
        internal static string ErrorLabelPageNotFound {
            get {
                return ResourceManager.GetString("ErrorLabelPageNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A reusable label already exists at this position..
        /// </summary>
        internal static string ErrorLabelReusableAlreadyExistsAtPosition {
            get {
                return ResourceManager.GetString("ErrorLabelReusableAlreadyExistsAtPosition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not a valid reusable label class..
        /// </summary>
        internal static string ErrorLabelReusableInvalidClass {
            get {
                return ResourceManager.GetString("ErrorLabelReusableInvalidClass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid reusable label name..
        /// </summary>
        internal static string ErrorLabelReusableInvalidName {
            get {
                return ResourceManager.GetString("ErrorLabelReusableInvalidName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Couldn&apos;t get value for reusable label &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorLabelReusableNotFound {
            get {
                return ResourceManager.GetString("ErrorLabelReusableNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Macro {0} already defined..
        /// </summary>
        internal static string ErrorMacroAlreadyDefined {
            get {
                return ResourceManager.GetString("ErrorMacroAlreadyDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Already at the top module level..
        /// </summary>
        internal static string ErrorModuleAlreadyAtTopLevel {
            get {
                return ResourceManager.GetString("ErrorModuleAlreadyAtTopLevel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can only load and compile a file during the initial pass..
        /// </summary>
        internal static string ErrorOnlyLoadDuringInitialPass {
            get {
                return ResourceManager.GetString("ErrorOnlyLoadDuringInitialPass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Output filename not specified..
        /// </summary>
        internal static string ErrorOutputFilenameNotSet {
            get {
                return ResourceManager.GetString("ErrorOutputFilenameNotSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Plugin &apos;{0}&apos; already loaded..
        /// </summary>
        internal static string ErrorPluginAlreadyAliased {
            get {
                return ResourceManager.GetString("ErrorPluginAlreadyAliased", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Plugin &apos;{0}&apos; not found..
        /// </summary>
        internal static string ErrorPluginNotFound {
            get {
                return ResourceManager.GetString("ErrorPluginNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid build configuration component &apos;{0}&apos;..
        /// </summary>
        internal static string ErrorProjectInvalidBuildConfigurationComponent {
            get {
                return ResourceManager.GetString("ErrorProjectInvalidBuildConfigurationComponent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not load template {0}..
        /// </summary>
        internal static string ErrorProjectTemplateNotFound {
            get {
                return ResourceManager.GetString("ErrorProjectTemplateNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type can only be set to TokenType.Instruction by assembler plugins..
        /// </summary>
        internal static string ErrorTokenTypeToInstruction {
            get {
                return ResourceManager.GetString("ErrorTokenTypeToInstruction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No name specified..
        /// </summary>
        internal static string ErrorUnspecifiedName {
            get {
                return ResourceManager.GetString("ErrorUnspecifiedName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Plugin &apos;{0}&apos; loaded twice..
        /// </summary>
        internal static string WarningPluginLoadedTwice {
            get {
                return ResourceManager.GetString("WarningPluginLoadedTwice", resourceCulture);
            }
        }
    }
}
