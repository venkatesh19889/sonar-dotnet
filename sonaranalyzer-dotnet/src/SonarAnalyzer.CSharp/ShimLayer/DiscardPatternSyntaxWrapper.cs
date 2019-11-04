﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace SonarAnalyzer.ShimLayer.CSharp
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    internal struct DiscardPatternSyntaxWrapper : ISyntaxWrapper<CSharpSyntaxNode>
    {
        internal const string WrappedTypeName = "Microsoft.CodeAnalysis.CSharp.Syntax.DiscardPatternSyntax";
        private static readonly Type WrappedType;

        private static readonly Func<CSharpSyntaxNode, SyntaxToken> UnderscoreTokenAccessor;
        private static readonly Func<CSharpSyntaxNode, SyntaxToken, CSharpSyntaxNode> WithUnderscoreTokenAccessor;

        private readonly CSharpSyntaxNode node;

        static DiscardPatternSyntaxWrapper()
        {
            WrappedType = WrapperHelper.GetWrappedType(typeof(DiscardPatternSyntaxWrapper));
            UnderscoreTokenAccessor = LightupHelpers.CreateSyntaxPropertyAccessor<CSharpSyntaxNode, SyntaxToken>(WrappedType, nameof(UnderscoreToken));
            WithUnderscoreTokenAccessor = LightupHelpers.CreateSyntaxWithPropertyAccessor<CSharpSyntaxNode, SyntaxToken>(WrappedType, nameof(UnderscoreToken));
        }

        private DiscardPatternSyntaxWrapper(CSharpSyntaxNode node)
        {
            this.node = node;
        }

        public CSharpSyntaxNode SyntaxNode => this.node;

        public SyntaxToken UnderscoreToken
        {
            get
            {
                return UnderscoreTokenAccessor(this.SyntaxNode);
            }
        }

        public static explicit operator DiscardPatternSyntaxWrapper(PatternSyntaxWrapper node)
        {
            return (DiscardPatternSyntaxWrapper)node.SyntaxNode;
        }

        public static explicit operator DiscardPatternSyntaxWrapper(SyntaxNode node)
        {
            if (node == null)
            {
                return default;
            }

            if (!IsInstance(node))
            {
                throw new InvalidCastException($"Cannot cast '{node.GetType().FullName}' to '{WrappedTypeName}'");
            }

            return new DiscardPatternSyntaxWrapper((CSharpSyntaxNode)node);
        }

        public static implicit operator PatternSyntaxWrapper(DiscardPatternSyntaxWrapper wrapper)
        {
            return PatternSyntaxWrapper.FromUpcast(wrapper.node);
        }

        public static implicit operator CSharpSyntaxNode(DiscardPatternSyntaxWrapper wrapper)
        {
            return wrapper.node;
        }

        public static bool IsInstance(SyntaxNode node)
        {
            return node != null && LightupHelpers.CanWrapNode(node, WrappedType);
        }

        public DiscardPatternSyntaxWrapper WithUnderscoreToken(SyntaxToken identifier)
        {
            return new DiscardPatternSyntaxWrapper(WithUnderscoreTokenAccessor(this.SyntaxNode, identifier));
        }
    }
}
