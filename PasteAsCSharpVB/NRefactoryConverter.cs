using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;

namespace PasteAsCSharpVB
{
	class NRefactoryConverter : IConvertCode
	{

		public string ConverterName
		{
			get { return "NRefactory"; }
		}
		public string Convert(string code)
		{
			return ConvertCodeSnippet(code, true);
		}

		public string ConvertCodeSnippet(string codeToConvert, bool csharpToVb)
		{
			//StringReader input = new StringReader(codeToConvert);
			SnippetParser parser = default(SnippetParser);
			List<ISpecial> specials;

			
			parser = new SnippetParser((csharpToVb ? SupportedLanguage.CSharp : SupportedLanguage.VBNet));

			parser.Parse(codeToConvert);
			//specials = parser.Lexer.SpecialTracker.RetrieveSpecials();

			//if ((parser.Errors.Count > 0))
			//{
			//    return parser.Errors.ErrorOutput;
			//}

			INode node = parser.Parse(codeToConvert);
			// parser.Errors.ErrorOutput contains syntax errors, if any
			// parser.Specials is the list of comments, preprocessor directives etc.
			if (csharpToVb)
				PreprocessingDirective.CSharpToVB(parser.Specials);
			else
				PreprocessingDirective.VBToCSharp(parser.Specials);
			// Convert C# constructs to VB.NET:
			//node.AcceptVisitor(new CSharpConstructsVisitor(), null);
			//node.AcceptVisitor(new ToVBNetConvertVisitor(), null);

			IOutputAstVisitor output = output = (csharpToVb ? (IOutputAstVisitor)new VBNetOutputVisitor() : (IOutputAstVisitor)new CSharpOutputVisitor());
			using (SpecialNodesInserter.Install(parser.Specials, output))
			{
				node.AcceptVisitor(output, null);
			}

			/*CompilationUnit cu = default(CompilationUnit);
			IOutputAstVisitor output = default(IOutputAstVisitor);
			cu = parser.CompilationUnit;

			output = (csharpToVb ? (IOutputAstVisitor)new VBNetOutputVisitor() : (IOutputAstVisitor)new CSharpOutputVisitor());
			SpecialNodesInserter.Install(specials, output);
			cu.AcceptVisitor(output, DBNull.Value);*/
			return output.Text;
		}

	}
}
