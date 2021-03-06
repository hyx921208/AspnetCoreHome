// This file was processed with Internalizer tool and should not be edited manually

using System;
using System.Buffers;
using System.Runtime;

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.AspNetCore.Server.Kestrel.Internal.System.IO.Pipelines
{
    public interface IReadableBufferAwaiter
    {
        bool IsCompleted { get; }

        ReadResult GetResult();

        void OnCompleted(Action continuation);
    }
}
