﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace SonarAnalyzer.ShimLayer.CSharp
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal struct SubpatternSyntaxWrapper : ISyntaxWrapper<CSharpSyntaxNode>
    {
        internal const string WrappedTypeName = "Microsoft.CodeAnalysis.CSharp.Syntax.SubpatternSyntax";
        private static readonly Type WrappedType;

        private static readonly Func<CSharpSyntaxNode, NameColonSyntax> NameColonAccessor;
        private static readonly Func<CSharpSyntaxNode, CSharpSyntaxNode> PatternAccessor;

        private static readonly Func<CSharpSyntaxNode, NameColonSyntax, CSharpSyntaxNode> WithNameColonAccessor;
        private static readonly Func<CSharpSyntaxNode, CSharpSyntaxNode, CSharpSyntaxNode> WithPatternAccessor;

        private readonly CSharpSyntaxNode node;

        static SubpatternSyntaxWrapper()
        {
            WrappedType = WrapperHelper.GetWrappedType(typeof(SubpatternSyntaxWrapper));
            NameColonAccessor = LightupHelpers.CreateSyntaxPropertyAccessor<CSharpSyntaxNode, NameColonSyntax>(WrappedType, nameof(NameColon));
            PatternAccessor = LightupHelpers.CreateSyntaxPropertyAccessor<CSharpSyntaxNode, CSharpSyntaxNode>(WrappedType, nameof(Pattern));

            WithNameColonAccessor = LightupHelpers.CreateSyntaxWithPropertyAccessor<CSharpSyntaxNode, NameColonSyntax>(WrappedType, nameof(NameColon));
            WithPatternAccessor = LightupHelpers.CreateSyntaxWithPropertyAccessor<CSharpSyntaxNode, CSharpSyntaxNode>(WrappedType, nameof(Pattern));
        }

        private SubpatternSyntaxWrapper(CSharpSyntaxNode node)
        {
            this.node = node;
        }

        public CSharpSyntaxNode SyntaxNode => this.node;

        public NameColonSyntax NameColon
        {
            get
            {
                return NameColonAccessor(this.SyntaxNode);
            }
        }

        public PatternSyntaxWrapper Pattern
        {
            get
            {
                return (PatternSyntaxWrapper)PatternAccessor(this.SyntaxNode);
            }
        }

        public static explicit operator SubpatternSyntaxWrapper(SyntaxNode node)
        {
            if (node == null)
            {
                return default;
            }

            if (!IsInstance(node))
            {
                throw new InvalidCastException($"Cannot cast '{node.GetType().FullName}' to '{WrappedTypeName}'");
            }

            return new SubpatternSyntaxWrapper((CSharpSyntaxNode)node);
        }

        public static implicit operator CSharpSyntaxNode(SubpatternSyntaxWrapper wrapper)
        {
            return wrapper.node;
        }

        public static bool IsInstance(SyntaxNode node)
        {
            return node != null && LightupHelpers.CanWrapNode(node, WrappedType);
        }

        public SubpatternSyntaxWrapper WithNameColon(NameColonSyntax nameColon)
        {
            return new SubpatternSyntaxWrapper(WithNameColonAccessor(this.SyntaxNode, nameColon));
        }

        public SubpatternSyntaxWrapper WithPattern(PatternSyntaxWrapper pattern)
        {
            return new SubpatternSyntaxWrapper(WithPatternAccessor(this.SyntaxNode, pattern));
        }
    }
}
