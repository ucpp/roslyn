﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Structure;

namespace Microsoft.CodeAnalysis.CSharp.Structure
{
    internal class EventDeclarationStructureProvider : AbstractSyntaxNodeStructureProvider<EventDeclarationSyntax>
    {
        protected override void CollectBlockSpans(
            EventDeclarationSyntax eventDeclaration,
            ArrayBuilder<BlockSpan> spans,
            OptionSet options,
            CancellationToken cancellationToken)
        {
            CSharpStructureHelpers.CollectCommentBlockSpans(eventDeclaration, spans);

            // fault tolerance
            if (eventDeclaration.AccessorList == null ||
                eventDeclaration.AccessorList.IsMissing ||
                eventDeclaration.AccessorList.OpenBraceToken.IsMissing ||
                eventDeclaration.AccessorList.CloseBraceToken.IsMissing)
            {
                return;
            }

            spans.AddIfNotNull(CSharpStructureHelpers.CreateBlockSpan(
                eventDeclaration,
                eventDeclaration.Identifier,
                autoCollapse: true,
                type: BlockTypes.Member,
                isCollapsible: true));
        }
    }
}
