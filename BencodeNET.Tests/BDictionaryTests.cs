﻿using System;
using System.Collections.Generic;
using BencodeNET.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BencodeNET.Tests
{
    [TestClass]
    public class BDictionaryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullValue()
        {
            var dict = new BDictionary();
            dict.Add("key", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullObjectValue()
        {
            var dict = new BDictionary();
            dict.Add("key", (IBObject)null);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddKeyValuePairWithNullValue()
        {
            var dict = new BDictionary();
            dict.Add(new KeyValuePair<BString, IBObject>("key", null));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetNullValue()
        {
            var dict = new BDictionary();
            dict.Add("key", "value");
            dict["key"] = null;
        }

        [TestMethod]
        public void EqualsBDictionary()
        {
            var bdict1 = new BDictionary
            {
                {"foobar", "Hello World!"},
                {"number", 747},
                {"key", "value"}
            };

            var bdict2 = new BDictionary
            {
                {"foobar", "Hello World!"},
                {"number", 747},
                {"key", "value"}
            };

            // Different order, but keys are sorted alphabetically so it is still equal
            var bdict3 = new BDictionary
            {
                {"number", 747},
                {"foobar", "Hello World!"},
                {"key", "value"}
            };

            var bdict4 = new BDictionary
            {
                {"foobar", "Hello World!"},
                {"number", 666},
                {"key", "value"}
            };

            Assert.AreEqual(bdict1, bdict2);
            Assert.AreEqual(bdict1, bdict3);
            Assert.AreEqual(bdict2, bdict3);
            Assert.AreNotEqual(bdict1, bdict4);
            Assert.AreNotEqual(bdict2, bdict4);
            Assert.AreNotEqual(bdict3, bdict4);
        }

        [TestMethod]
        public void EqualsBDictionaryWithEqualsOperator()
        {
            var bdict1 = new BDictionary
            {
                {"foobar", "Hello World!"},
                {"number", 747},
                {"key", "value"}
            };

            var bdict2 = new BDictionary
            {
                {"foobar", "Hello World!"},
                {"number", 747},
                {"key", "value"}
            };

            // Different order, but keys are sorted alphabetically so it is still equal
            var bdict3 = new BDictionary
            {
                {"number", 747},
                {"foobar", "Hello World!"},
                {"key", "value"}
            };

            var bdict4 = new BDictionary
            {
                {"foobar", "Hello World!"},
                {"number", 666},
                {"key", "value"}
            };

            Assert.IsTrue(bdict1 == bdict2);
            Assert.IsTrue(bdict1 == bdict3);
            Assert.IsTrue(bdict2 == bdict3);
            Assert.IsTrue(bdict1 != bdict4);
            Assert.IsTrue(bdict2 != bdict4);
            Assert.IsTrue(bdict3 != bdict4);
        }

        [TestMethod]
        public void Encode_Simple()
        {
            var bdict = new BDictionary
            {
                {"foobar", "Hello World!"},
                {"number", 747},
                {"key", "value"}
            };

            // Keys should be sorted in lexical order
            var expected = "d6:foobar12:Hello World!3:key5:value6:numberi747ee";
            var actual = bdict.Encode();
            
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Encode_EmptyDictionary()
        {
            var bdict = new BDictionary();
            Assert.AreEqual("de", bdict.Encode());
        }

        [TestMethod]
        public void Encode_Complex()
        {
            var bdict = new BDictionary
            {
                {"spam", "egg"},
                {
                    "A List", new BList
                    {
                        "foo",
                        "bar",
                        123,
                        new BDictionary
                        {
                            {"more spam", "more eggs"}
                        }
                    }
                },
                {
                    "foobar", new BDictionary
                    {
                        {"numbers", new BList {1, 2, 3}}
                    }
                }
            };

            // Keys should be sorted in lexical order
            var expected = "d6:A Listl3:foo3:bari123ed9:more spam9:more eggsee6:foobard7:numbersli1ei2ei3eee4:spam3:egge";
            var actual = bdict.Encode();
            
            Assert.AreEqual(expected, actual);
        }
    }
}
