﻿// Copyright(c) Microsoft Corporation.All rights reserved.
// Licensed under the MIT License.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Toolkit.Uwp.UI.Lottie.LottieMetadata
{
    /// <summary>
    /// A frame location in a Lottie composition.
    /// </summary>
#if PUBLIC_LottieMetadata
    public
#endif
    readonly struct Frame : IComparable<Frame>
    {
        readonly Duration _context;

        internal Frame(Duration context, double number)
        {
            Number = number;
            _context = context;
        }

        /// <summary>
        /// The frame number.
        /// </summary>
        public double Number { get; }

        /// <summary>
        /// The location as a proportion of the Lottie composition.
        /// </summary>
        public double Progress => Number / _context.Frames;

        /// <summary>
        /// The location as a time offset from the start of the Lottie composition.
        /// </summary>
        public TimeSpan Time => Progress * _context.Time;

        public static Frame operator +(Frame frame, Duration duration)
        {
            return new Frame(frame._context, frame.Number + duration.Frames);
        }

        public static Duration operator -(Frame frameA, Frame frameB)
        {
            // The frames must refer to the same Duration otherwise it makes no sense
            // to subtract them.
            if (frameA._context != frameB._context)
            {
                throw new ArgumentException();
            }

            return new Duration(frameA.Number - frameB.Number, frameA._context);
        }

        public override string ToString() => $"{Number}/{Progress}/{Time}";

        public int CompareTo(Frame other) => Number.CompareTo(other.Number);
    }
}
