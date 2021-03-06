﻿using System.IO;
using System.Text;
using BencodeNET.IO;
using BencodeNET.Objects;
using BencodeNET.Parsing;
using NSubstitute;
using Xunit;

namespace BencodeNET.Tests.Parsing
{
    public class BObjectParserTests
    {
        [Theory]
        [InlineAutoMockedData]
        public void IBObjectParser_Parse_String_CallsOverriddenParse(IBObjectParser<IBObject> parserMock)
        {
            var parser = new MockBObjectParser(parserMock) as IBObjectParser;

            parser.ParseString("bencoded string");

            parserMock.Received().Parse(Arg.Any<BencodeStream>());
        }

        [Theory]
        [InlineAutoMockedData]
        public void IBObjectParser_Parse_Stream_CallsOverriddenParse(IBObjectParser<IBObject> parserMock)
        {
            var parser = new MockBObjectParser(parserMock) as IBObjectParser;
            var bytes = Encoding.UTF8.GetBytes("bencoded string");

            using (var stream = new MemoryStream(bytes))
            {
                parser.Parse(stream);
            }

            parserMock.Received().Parse(Arg.Any<BencodeStream>());
        }

        [Theory]
        [InlineAutoMockedData]
        public void Parse_String_CallsOverriddenParse(IBObjectParser<IBObject> parserMock)
        {
            var parser = new MockBObjectParser(parserMock);

            parser.ParseString("bencoded string");

            parserMock.Received().Parse(Arg.Any<BencodeStream>());
        }

        [Theory]
        [InlineAutoMockedData]
        public void Parse_Stream_CallsOverriddenParse(IBObjectParser<IBObject> parserMock)
        {
            var parser = new MockBObjectParser(parserMock);
            var bytes = Encoding.UTF8.GetBytes("bencoded string");

            using (var stream = new MemoryStream(bytes))
            {
                parser.Parse(stream);
            }

            parserMock.Received().Parse(Arg.Any<BencodeStream>());
        }

        class MockBObjectParser : BObjectParser<IBObject>
        {
            public MockBObjectParser(IBObjectParser<IBObject> substitute)
            {
                Substitute = substitute;
            }

            public IBObjectParser<IBObject> Substitute { get; set; }

            protected override Encoding Encoding => Encoding.UTF8;

            public override IBObject Parse(BencodeStream stream)
            {
                return Substitute.Parse(stream);
            }
        }
    }
}
