using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace RefactoringEssentials.CSharp.Diagnostics
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    [NotPortedYet]
    public class XmlDocAnalyzer : DiagnosticAnalyzer
    {
        static readonly DiagnosticDescriptor descriptor = new DiagnosticDescriptor(
            CSharpDiagnosticIDs.XmlDocAnalyzerID,
            GettextCatalog.GetString("Validate Xml docs"),
            "{0}",
            DiagnosticAnalyzerCategories.CompilerWarnings,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            helpLinkUri: HelpLink.CreateFor(CSharpDiagnosticIDs.XmlDocAnalyzerID)
        );

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(descriptor);

        public override void Initialize(AnalysisContext context)
        {
            //context.RegisterSyntaxNodeAction(
            //	(nodeContext) => {
            //		Diagnostic diagnostic;
            //		if (TryGetDiagnostic (nodeContext, out diagnostic)) {
            //			nodeContext.ReportDiagnostic(diagnostic);
            //		}
            //	}, 
            //	new SyntaxKind[] { SyntaxKind.None }
            //);
        }

        static bool TryGetDiagnostic(SyntaxNodeAnalysisContext nodeContext, out Diagnostic diagnostic)
        {
            diagnostic = default(Diagnostic);
            if (nodeContext.IsFromGeneratedCode())
                return false;
            //var node = nodeContext.Node as ;
            //diagnostic = Diagnostic.Create (descriptor, node.GetLocation ());
            //return true;
            return false;
        }

        //		class GatherVisitor : GatherVisitorBase<XmlDocAnalyzer>
        //		{
        //			//readonly List<Comment> storedXmlComment = new List<Comment>();

        //			public GatherVisitor(SemanticModel semanticModel, Action<Diagnostic> addDiagnostic, CancellationToken cancellationToken)
        //				: base(semanticModel, addDiagnostic, cancellationToken)
        //			{
        //			}

        ////			void InvalideXmlComments()
        ////			{
        ////				if (storedXmlComment.Count == 0)
        ////					return;
        ////				var from = storedXmlComment.First().StartLocation;
        ////				var to = storedXmlComment.Last().EndLocation;
        ////				AddDiagnosticAnalyzer(new CodeIssue(
        ////					from,
        ////					to,
        ////					ctx.TranslateString("Xml comment is not placed before a valid language element"),
        ////					ctx.TranslateString("Remove comment"),
        ////					script => {
        ////						var startOffset = script.GetCurrentOffset(from);
        ////						var endOffset = script.GetCurrentOffset(to);
        ////						endOffset += ctx.GetLineByOffset(endOffset).DelimiterLength;
        ////						script.RemoveText(startOffset, endOffset - startOffset);
        ////					}
        ////				));
        ////				storedXmlComment.Clear();
        ////			}
        ////
        ////			public override void VisitComment(Comment comment)
        ////			{
        ////				if (comment.CommentType == CommentType.Documentation)
        ////					storedXmlComment.Add(comment);
        ////			}
        ////
        ////			public override void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        ////			{
        ////				InvalideXmlComments();
        ////				base.VisitNamespaceDeclaration(namespaceDeclaration);
        ////			}
        ////
        ////			public override void VisitUsingDeclaration(UsingDeclaration usingDeclaration)
        ////			{
        ////				InvalideXmlComments();
        ////				base.VisitUsingDeclaration(usingDeclaration);
        ////			}
        ////
        ////			public override void VisitUsingAliasDeclaration(UsingAliasDeclaration usingDeclaration)
        ////			{
        ////				InvalideXmlComments();
        ////				base.VisitUsingAliasDeclaration(usingDeclaration);
        ////			}
        ////
        ////			public override void VisitExternAliasDeclaration(ExternAliasDeclaration externAliasDeclaration)
        ////			{
        ////				InvalideXmlComments();
        ////				base.VisitExternAliasDeclaration(externAliasDeclaration);
        ////			}
        ////
        ////			TextLocation TranslateOffset(int offset)
        ////			{
        ////				int line = storedXmlComment.First().StartLocation.Line;
        ////				foreach (var cmt in storedXmlComment) {
        ////					var next = offset - cmt.Content.Length - "\n".Length;
        ////					if (next <= 0)
        ////						return new TextLocation(line, cmt.StartLocation.Column + "///".Length + offset);
        ////					offset = next;
        ////					line++;
        ////				}
        ////				return TextLocation.Empty;
        ////			}
        ////
        ////			void AddXmlIssue(int offset, int length, string str)
        ////			{
        ////				var textLocation = TranslateOffset(offset);
        ////				var textLocation2 = TranslateOffset(offset + length);
        ////				if (textLocation.IsEmpty && textLocation2.IsEmpty) {
        ////					AddDiagnosticAnalyzer(new CodeIssue(storedXmlComment.Last(), str));
        ////				} else if (textLocation.IsEmpty) {
        ////					AddDiagnosticAnalyzer(new CodeIssue(storedXmlComment.First().StartLocation, textLocation2, str));
        ////				} else if (textLocation2.IsEmpty) {
        ////					AddDiagnosticAnalyzer(new CodeIssue(textLocation, storedXmlComment.Last().EndLocation, str));
        ////				} else {
        ////					AddDiagnosticAnalyzer(new CodeIssue(textLocation, textLocation2, str));
        ////				}
        ////			}
        ////			readonly StringTextSource emptySource = new StringTextSource("");
        ////			void CheckXmlDoc(AstNode node)
        ////			{
        ////				ResolveResult resolveResult = ctx.Resolve(node);
        ////				IEntity member = null;
        ////				if (resolveResult is TypeResolveResult)
        ////					member = resolveResult.Type.GetDefinition();
        ////				if (resolveResult is MemberResolveResult)
        ////					member = ((MemberResolveResult)resolveResult).Member;
        ////				var xml = new StringBuilder();
        ////				var firstline = "<root>\n";
        ////				xml.Append(firstline);
        ////				foreach (var cmt in storedXmlComment)
        ////					xml.Append(cmt.Content + "\n");
        ////				xml.Append("</root>\n");
        ////
        ////				var doc = new AXmlParser().Parse(new StringTextSource(xml.ToString()));
        ////
        ////				var stack = new Stack<AXmlObject>();
        ////				stack.Push(doc);
        ////				foreach (var err in doc.SyntaxErrors)
        ////					AddXmlIssue(err.StartOffset - firstline.Length, err.EndOffset - err.StartOffset, err.Description);
        ////
        ////				while (stack.Count > 0) {
        ////					var cur = stack.Pop();
        ////					var el = cur as AXmlElement;
        ////					if (el != null) {
        ////						switch (el.Name) {
        ////							case "typeparam":
        ////							case "typeparamref":
        ////								var name = el.Attributes.FirstOrDefault(attr => attr.Name == "name");
        ////								if (name == null)
        ////									break;
        ////								if (member != null && member.SymbolKind == SymbolKind.TypeDefinition) {
        ////									var type = (ITypeDefinition)member;
        ////									if (!type.TypeArguments.Any(arg => arg.Name == name.Value)) {
        ////										AddXmlIssue(name.ValueSegment.Offset - firstline.Length + 1, name.ValueSegment.Length - 2, string.Format(ctx.TranslateString("Type parameter '{0}' not found"), name.Value));
        ////									}
        ////								}
        ////								break;
        ////							case "param":
        ////							case "paramref":
        ////								name = el.Attributes.FirstOrDefault(attr => attr.Name == "name");
        ////								if (name == null)
        ////									break;
        ////								var m = member as IParameterizedMember;
        ////								if (m != null && m.Parameters.Any(p => p.Name == name.Value))
        ////									break;
        ////								if (name.Value == "value" && member != null && (member.SymbolKind == SymbolKind.Property || member.SymbolKind == SymbolKind.Indexer || member.SymbolKind == SymbolKind.Event) && el.Name == "paramref")
        ////									break;
        ////								AddXmlIssue(name.ValueSegment.Offset - firstline.Length + 1, name.ValueSegment.Length - 2, string.Format(ctx.TranslateString("Parameter '{0}' not found"), name.Value));
        ////								break;
        ////							case "exception":
        ////							case "seealso":
        ////							case "see":
        ////								var cref = el.Attributes.FirstOrDefault(attr => attr.Name == "cref");
        ////								if (cref == null)
        ////									break;
        ////								try {
        ////									var trctx = ctx.Resolver.TypeResolveContext;
        ////									if (member is IMember)
        ////										trctx = trctx.WithCurrentTypeDefinition(member.DeclaringTypeDefinition).WithCurrentMember((IMember)member);
        ////									if (member is ITypeDefinition)
        ////										trctx = trctx.WithCurrentTypeDefinition((ITypeDefinition)member);
        ////									var state = ctx.Resolver.GetResolverStateBefore(node);
        ////									if (state.CurrentUsingScope != null)
        ////										trctx = trctx.WithUsingScope(state.CurrentUsingScope);
        ////									var cdc = new CSharpDocumentationComment (emptySource, trctx);
        ////									var entity = cdc.ResolveCref(cref.Value);
        ////
        ////									if (entity == null) {
        ////										AddXmlIssue(cref.ValueSegment.Offset - firstline.Length + 1, cref.ValueSegment.Length - 2, string.Format(ctx.TranslateString("Cannot find reference '{0}'"), cref.Value));
        ////									}
        ////								} catch (Exception e) {
        ////									AddXmlIssue(cref.ValueSegment.Offset - firstline.Length + 1, cref.ValueSegment.Length - 2, string.Format(ctx.TranslateString("Reference parsing error '{0}'."), e.Message));
        ////								}
        ////								break;
        ////
        ////						}
        ////					}
        ////					foreach (var child in cur.Children)
        ////						stack.Push(child);
        ////				}
        ////				storedXmlComment.Clear();
        ////			}
        ////
        ////			protected virtual void VisitXmlChildren(AstNode node, Action checkDocumentationAction)
        ////			{
        ////				AstNode next;
        ////				var child = node.FirstChild;
        ////				while (child != null && (child is Comment || child.Role == Roles.NewLine)) {
        ////					next = child.NextSibling;
        ////					child.AcceptVisitor(this);
        ////					child = next;
        ////				}
        ////				checkDocumentationAction();
        ////
        ////				for (; child != null; child = next) {
        ////					// Store next to allow the loop to continue
        ////					// if the visitor removes/replaces child.
        ////					next = child.NextSibling;
        ////					child.AcceptVisitor(this);
        ////				}
        ////				InvalideXmlComments();
        ////			}
        ////
        ////			protected virtual void VisitXmlChildren(AstNode node)
        ////			{
        ////				VisitXmlChildren(node, () => CheckXmlDoc(node));
        ////			}
        ////
        ////			public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        ////			{
        ////				VisitXmlChildren(typeDeclaration);
        ////			}
        ////
        ////			public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        ////			{
        ////				VisitXmlChildren(methodDeclaration);
        ////			}
        ////
        ////			public override void VisitDelegateDeclaration(DelegateDeclaration delegateDeclaration)
        ////			{
        ////				VisitXmlChildren(delegateDeclaration);
        ////			}
        ////
        ////			public override void VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
        ////			{
        ////				VisitXmlChildren(constructorDeclaration);
        ////			}
        ////
        ////			public override void VisitCustomEventDeclaration(CustomEventDeclaration eventDeclaration)
        ////			{
        ////				VisitXmlChildren(eventDeclaration);
        ////			}
        ////
        ////			public override void VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
        ////			{
        ////				VisitXmlChildren(destructorDeclaration);
        ////			}
        ////
        ////			public override void VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        ////			{
        ////				VisitXmlChildren(enumMemberDeclaration);
        ////			}
        ////
        ////			public override void VisitEventDeclaration(EventDeclaration eventDeclaration)
        ////			{
        ////				VisitXmlChildren(eventDeclaration, () => {
        ////					foreach (var e in eventDeclaration.Variables) {
        ////						CheckXmlDoc(e);
        ////					}
        ////				});
        ////			}
        ////
        ////			public override void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
        ////			{
        ////				VisitXmlChildren(fieldDeclaration, () => {
        ////					foreach (var e in fieldDeclaration.Variables) {
        ////						CheckXmlDoc(e);
        ////					}
        ////				});
        ////			}
        ////
        ////			public override void VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
        ////			{
        ////				VisitXmlChildren(indexerDeclaration);
        ////			}
        ////
        ////			public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
        ////			{
        ////				VisitXmlChildren(propertyDeclaration);
        ////			}
        ////
        ////			public override void VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
        ////			{
        ////				VisitXmlChildren(operatorDeclaration);
        ////			}
        //		}
    }


}