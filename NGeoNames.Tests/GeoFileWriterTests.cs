﻿using NUnit.Framework;
using NGeoNames;
using System;
using System.Linq;
using System.Text;
using NGeoNames.Parsers;

namespace NGeoNames.Tests
{
    [TestFixture]
    public class GeoFileWriterTests
    {
        private static readonly CustomEntity[] testvalues = new [] {
            new CustomEntity { Data = new [] { "Data L1☃F1", "Data L1☃F2", "Data L1☃F3"}},
            new CustomEntity { Data = new [] { "Data L2☃F1", "Data L2☃F2", "Data L2☃F3"}},
            new CustomEntity { Data = new [] { "Data L3☃F1", "Data L3☃F2", "Data L3☃F3"}},
        };

        [Test]
        public void GeoFileWriter_ComposesFileCorrectly1()
        {
            new GeoFileWriter().WriteRecords<CustomEntity>(@"testdata\test_geofilewritercustom1.txt", testvalues, new CustomComposer(Encoding.UTF8, ','));

            var gf = new GeoFileReader();
            var target = gf.ReadRecords<CustomEntity>(@"testdata\test_geofilewritercustom1.txt", new CustomParser(3, 0, new[] { ',' }, Encoding.UTF8, false)).ToArray();
            Assert.AreEqual(3, target.Length);
            CollectionAssert.AreEqual(testvalues, target, new CustomEntityComparer());
        }

        [Test]
        public void GeoFileWriter_ComposesFileCorrectly2()
        {
            new GeoFileWriter().WriteRecords<CustomEntity>(@"testdata\test_geofilewritercustom2.txt", testvalues, new CustomComposer(Encoding.UTF7, '!'));

            var gf = new GeoFileReader();
            var target = gf.ReadRecords<CustomEntity>(@"testdata\test_geofilewritercustom2.txt", new CustomParser(3, 0, new[] { '!' }, Encoding.UTF7, false)).ToArray();
            Assert.AreEqual(3, target.Length);
            CollectionAssert.AreEqual(testvalues, target, new CustomEntityComparer());
        }

        //[Test]
        //public void GeoFileWriter_ThrowsOnFailureWhenAutodetectingFileType()
        //{
        //    //When filetype == autodetect and an unknown extension is used an exception should be thrown
        //    Assert.Throws<NotSupportedException>(() => new GeoFileWriter().WriteRecords<CustomEntity>(@"testdata\invalid.out.ext", testvalues, new CustomComposer(Encoding.UTF8, '\t')));
        //}

        //[Test]
        //public void GeoFileWriter_DoesNotThrowOnInvalidExtensionButSpecifiedFileType()
        //{
        //    //When filetype is specified and an unknown extension is used it should be written fine
        //    new GeoFileWriter().WriteRecords<CustomEntity>(@"testdata\invalid.out.ext", testvalues, new CustomComposer(Encoding.UTF8, '\t'), FileType.Plain);
        //}

        //[Test]
        //[ExpectedException(typeof(NotSupportedException))]
        //public void GeoFileWriter_ThrowsOnUnknownSpecifiedFileType()
        //{
        //    //When and unknown filetype is specified an exception should be thrown
        //    new GeoFileWriter().WriteRecords<CustomEntity>(@"testdata\invalid.out.ext", testvalues, new CustomComposer(Encoding.UTF8, '\t'), (FileType)999);
        //}
    }
}
