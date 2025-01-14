using System;

namespace Tests.Diagnostics
{
    class Program
    {
        private string[] list =
        {
            "",       // Compliant

            // 0x00
            "\0",     // Compliant
            " ",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\0'.}}
//          ^^^

            // 0x01
            "\u0001", // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u0001'.}}

            // 0x02
            "((\u0002))", // Compliant
            "(())",      // Noncompliant {{Replace the control character at position 3 by its escape sequence '\u0002'.}}
//          ^^^^^^^

            // ...

            // 0x06
            "\u0006", // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u0006'.}}

            // 0x07
            "\a",     // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\a'.}}

            // 0x08
            "\b",     // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\b'.}}

            // 0x09
            "\t",     // Compliant
            "	",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\t'.}}

            // 0x0A
            "\n",     // Compliant
            // can not test unescaped \u000A

            // 0x0B
            "\v",     // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\v'.}}

            // 0x0C
            "\f",     // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\f'.}}

            // 0x0D
            "\r",     // Compliant
            // can not test unescaped \u000D

            // 0x0E
            "\u000E", // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u000E'.}}

            // 0x0F
            "\u000F", // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u000F'.}}

            // ...

            "\u001F", // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u001F'.}}

            "\u0020", // Compliant
            " ",      // Compliant

            "\u0021", // Compliant
            "!",      // Compliant

            // ...

            "\u007F", // Compliant
            "",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u007F'.}}

            "\u0080", // Compliant
            "",      // Compliant, range from \u0080 to \u009F is considered by some languages as control characters
                      // But it's not true for all encodings. For example, Windows-1250 code page encodes euro sign by using \u0080

            "\u0081", // Compliant
            "",      // Compliant

            //..

            "\u009E", // Compliant
            "",      // Compliant

            "\u0100", // Compliant
            "Ā",      // Compliant

            // ogham space mark
            "\u1680", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u1680'.}}

            // mongolian vowel separator
            "\u180E", // Compliant
            "᠎",       // Compliant, it is no longer classified as space character in Unicode 6.3.0

            // en quad
            "\u2000", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2000'.}}

            // em quad
            "\u2001", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2001'.}}

            // en space
            "\u2002", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2002'.}}

            // em space
            "\u2003", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2003'.}}

            // three-per-em space
            "\u2004", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2004'.}}

            // four-per-em space
            "\u2005", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2005'.}}

            // six-per-em space
            "\u2006", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2006'.}}

            // figure space
            "\u2007", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2007'.}}

            // punctuation space
            "\u2008", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2008'.}}

            // thin space
            "\u2009", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2009'.}}

            // hair space
            "\u200A", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u200A'.}}

            // zero width space
            "\u200B", // Compliant
            "​",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u200B'.}}

            // zero width non-joiner
            "\u200C", // Compliant
            "‌",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u200C'.}}

            // zero width joiner
            "\u200D", // Compliant
            "‍",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u200D'.}}

            // word joiner
            "\u2060", // Compliant
            "⁠",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u2060'.}}

            // line separator
            "\u2028", // Compliant
            // can not test unescaped \u2028

            // paragraph separator
            "\u2029", // Compliant
            // can not test unescaped \u2029

            // narrow no-break space
            "\u202F", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u202F'.}}

            // medium mathematical space
            "\u205F", // Compliant
            " ",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u205F'.}}

            // ideographic space
            "\u3000", // Compliant
            "　",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u3000'.}}

            // zero width non-breaking space
            "\uFEFF", // Compliant
            "﻿",       // Noncompliant {{Replace the control character at position 1 by its escape sequence '\uFEFF'.}}


            // interpolated string literal
            $"\t",            // Compliant
            $"{null}	{null}", // Noncompliant {{Replace the control character at position 1 by its escape sequence '\t'.}}
//                  ^

            // verbatim string literal
            @"", // Compliant even if this string contains the control character \u0001. Because it is part of the exception: verbatim string literals have no escape character mechanism

            @"
                line1
                line2
            ",

            // character positions
            "hello",      // Noncompliant {{Replace the control character at position 1 by its escape sequence '\u0001'.}}
            "hello",      // Noncompliant {{Replace the control character at position 4 by its escape sequence '\u0001'.}}
            "hello",      // Noncompliant {{Replace the control character at position 6 by its escape sequence '\u0001'.}}

            // string concatenation
            "prefix" +
            "" +          // Noncompliant
            "suffix"
        };
    }
}
