/*
 * SonarAnalyzer for .NET
 * Copyright (C) 2015-2019 SonarSource SA
 * mailto: contact AT sonarsource DOT com
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3 of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

extern alias csharp;

using System.Collections.Immutable;
using System.IO;
using csharp::SonarAnalyzer.Rules.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SonarAnalyzer.UnitTest.TestFramework;
using CS = Microsoft.CodeAnalysis.CSharp;

namespace SonarAnalyzer.UnitTest.Rules
{
    [TestClass]
    public class DeadStoresTest
    {
        [TestMethod]
        [TestCategory("Rule")]
        public void DeadStores()
        {
            var system = (MetadataReference)MetadataReference.CreateFromFile(
                Path.Combine(@"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\3.0.0-preview8-28405-07\ref\netcoreapp3.0",
                    "System.dll"));

            var runtime = (MetadataReference)MetadataReference.CreateFromFile(
                Path.Combine(@"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\3.0.0-preview8-28405-07\ref\netcoreapp3.0",
                    "System.Runtime.dll"));
            Verifier.VerifyAnalyzer(@"TestCases\DeadStores.cs",
                new DeadStores(),
                options: new[] { new CS.CSharpParseOptions(CS.LanguageVersion.Preview) },
                additionalReferences: new[] { system, runtime });
        }
    }
}
