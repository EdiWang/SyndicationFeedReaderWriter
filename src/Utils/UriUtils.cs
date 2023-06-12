// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace Edi.SyndicationFeed.ReaderWriter.Utils
{
    static class UriUtils
    {
        public static bool TryParse(string value, out Uri result)
        {
            return Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out result);
        }
    }
}
