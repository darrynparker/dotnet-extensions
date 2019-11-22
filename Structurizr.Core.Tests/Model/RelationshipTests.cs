﻿using Xunit;

namespace Structurizr.Core.Tests
{
    
    public class RelationshipTests : AbstractTestBase
    {

        private SoftwareSystem _softwareSystem1, _softwareSystem2;

        public RelationshipTests()
        {
            _softwareSystem1 = Model.AddSoftwareSystem("1", "Description");
            _softwareSystem2 = Model.AddSoftwareSystem("2", "Description");
        }

        [Fact]
        public void Test_Description_NeverReturnsNull()
        {
            Relationship relationship = new Relationship();
            relationship.Description = null;
            Assert.Equal("", relationship.Description);
        }

        [Fact]
        public void Test_Tags_WhenThereAreNoTags()
        {
            Relationship relationship = _softwareSystem1.Uses(_softwareSystem2, "uses");
            Assert.Equal("Relationship,Synchronous", relationship.Tags);
        }

        [Fact]
        public void Test_Tags_ReturnsTheListOfTags_WhenThereAreSomeTags()
        {
            Relationship relationship = _softwareSystem1.Uses(_softwareSystem2, "uses");
            relationship.AddTags("tag1", "tag2", "tag3");
            Assert.Equal("Relationship,Synchronous,tag1,tag2,tag3", relationship.Tags);
        }

        [Fact]
        public void Test_Tags_ClearsTheTags_WhenPassedNull()
        {
            Relationship relationship = _softwareSystem1.Uses(_softwareSystem2, "uses");
            relationship.Tags = null;
            Assert.Equal("Relationship", relationship.Tags);
        }

        [Fact]
        public void Test_AddTags_DoesNotDoAnything_WhenPassedNull()
        {
            Relationship relationship = _softwareSystem1.Uses(_softwareSystem2, "uses");
            relationship.AddTags((string)null);
            Assert.Equal("Relationship,Synchronous", relationship.Tags);

            relationship.AddTags(null, null, null);
            Assert.Equal("Relationship,Synchronous", relationship.Tags);
        }

        [Fact]
        public void Test_AddTags_AddsTags_WhenPassedSomeTags()
        {
            Relationship relationship = _softwareSystem1.Uses(_softwareSystem2, "uses");
            relationship.AddTags(null, "tag1", null, "tag2");
            Assert.Equal("Relationship,Synchronous,tag1,tag2", relationship.Tags);
        }

        [Fact]
        public void Test_InteractionStyle_ReturnsSynchronous_WhenNotExplicitlySet()
        {
            Relationship relationship = _softwareSystem1.Uses(_softwareSystem2, "uses");
            Assert.Equal(InteractionStyle.Synchronous, relationship.InteractionStyle);
        }

        [Fact]
        public void test_Tags_IncludesTheInteractionStyleWhenSpecified()
        {
            Relationship relationship = _softwareSystem1.Uses(_softwareSystem2, "Uses 1", "Technology");
            Assert.True(relationship.Tags.Contains(Tags.Synchronous));
            Assert.False(relationship.Tags.Contains(Tags.Asynchronous));

            relationship = _softwareSystem1.Uses(_softwareSystem2, "Uses 2", "Technology", InteractionStyle.Asynchronous);
            Assert.False(relationship.Tags.Contains(Tags.Synchronous));
            Assert.True(relationship.Tags.Contains(Tags.Asynchronous));
        }

    }
}
