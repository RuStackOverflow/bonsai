﻿using System.Collections.Generic;

namespace Bonsai.Areas.Front.Logic.Facts
{
    /// <summary>
    /// A group of correlated facts.
    /// </summary>
    public class FactDefinitionGroup
    {
        public FactDefinitionGroup(string id, string title, params FactDefinition[] facts)
        {
            Id = id;
            Title = title;
            Facts = facts;
        }

        /// <summary>
        /// Unique ID of the group.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Readable title of the group.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Nested fact definitions.
        /// </summary>
        public IReadOnlyList<FactDefinition> Facts { get; }
    }
}
