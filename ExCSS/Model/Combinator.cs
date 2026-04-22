// The MIT License (MIT)
//
// Copyright (c) 2024 Tyler Brinks
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using ExCSS.Enumerations;
using ExCSS.Selectors;

namespace ExCSS.Model;

public abstract class Combinator
{
    public static readonly Combinator Child = new ChildCombinator();
    public static readonly Combinator Deep = new DeepCombinator();
    public static readonly Combinator Descendent = new DescendentCombinator();
    public static readonly Combinator AdjacentSibling = new AdjacentSiblingCombinator();
    public static readonly Combinator Sibling = new SiblingCombinator();
    public static readonly Combinator Namespace = new NamespaceCombinator();
    public static readonly Combinator Column = new ColumnCombinator();
    public string Delimiter { get; protected set; }

    public virtual ISelector Change(ISelector selector)
    {
        return selector;
    }

    private sealed class ChildCombinator : Combinator
    {
        public ChildCombinator()
        {
            Delimiter = Combinators.Child;
        }
    }

    private sealed class DeepCombinator : Combinator
    {
        public DeepCombinator()
        {
            Delimiter = Combinators.Deep;
        }
    }

    private sealed class DescendentCombinator : Combinator
    {
        public DescendentCombinator()
        {
            Delimiter = Combinators.Descendent;
        }
    }

    private sealed class AdjacentSiblingCombinator : Combinator
    {
        public AdjacentSiblingCombinator()
        {
            Delimiter = Combinators.Adjacent;
        }
    }

    private sealed class SiblingCombinator : Combinator
    {
        public SiblingCombinator()
        {
            Delimiter = Combinators.Sibling;
        }
    }

    private sealed class NamespaceCombinator : Combinator
    {
        public NamespaceCombinator()
        {
            Delimiter = Combinators.Pipe;
        }

        public override ISelector Change(ISelector selector)
        {
            return NamespaceSelector.Create(selector.Text);
        }
    }

    private sealed class ColumnCombinator : Combinator
    {
        public ColumnCombinator()
        {
            Delimiter = Combinators.Column;
        }
    }
}