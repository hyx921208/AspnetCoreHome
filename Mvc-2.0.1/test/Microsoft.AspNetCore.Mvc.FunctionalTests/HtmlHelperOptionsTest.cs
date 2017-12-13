// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Testing;
using Xunit;

namespace Microsoft.AspNetCore.Mvc.FunctionalTests
{
    public class HtmlHelperOptionsTest : IClassFixture<MvcTestFixture<RazorWebSite.Startup>>
    {
        private const string UseDateTimeLocalTypeForDateTimeOffsetSwitch = "Switch.Microsoft.AspNetCore.Mvc.UseDateTimeLocalTypeForDateTimeOffset";

        public HtmlHelperOptionsTest(MvcTestFixture<RazorWebSite.Startup> fixture)
        {
            Client = fixture.Client;
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task AppWideDefaultsInViewAndPartialView()
        {
            // Arrange
            AppContext.TryGetSwitch(UseDateTimeLocalTypeForDateTimeOffsetSwitch, out var enabled);
            var expectedType = enabled ? "datetime-local" : "text";
            var expectedOffset = enabled ? string.Empty : "&#x2B;00:00";
            var expected =
$@"<div class=""validation-summary-errors""><validationSummaryElement>MySummary</validationSummaryElement>
<ul><li>A model error occurred.</li>
</ul></div>
<validationMessageElement class=""field-validation-error"">An error occurred.</validationMessageElement>
<input id=""Prefix!Property1"" name=""Prefix.Property1"" type=""text"" value="""" />
<div class=""editor-label""><label for=""MyDate"">MyDate</label></div>
<div class=""editor-field""><input class=""text-box single-line"" id=""MyDate"" name=""MyDate"" type=""{expectedType}"" value=""2000-01-02T03:04:05.060{expectedOffset}"" /> </div>

<div class=""validation-summary-errors""><validationSummaryElement>MySummary</validationSummaryElement>
<ul><li>A model error occurred.</li>
</ul></div>
<validationMessageElement class=""field-validation-error"">An error occurred.</validationMessageElement>
<input id=""Prefix!Property1"" name=""Prefix.Property1"" type=""text"" value="""" />
<div class=""editor-label""><label for=""MyDate"">MyDate</label></div>
<div class=""editor-field""><input class=""text-box single-line"" id=""MyDate"" name=""MyDate"" type=""{expectedType}"" value=""2000-01-02T03:04:05.060{expectedOffset}"" /> </div>

False";

            // Act
            var body = await Client.GetStringAsync("http://localhost/HtmlHelperOptions/HtmlHelperOptionsDefaultsInView");

            // Assert
            Assert.Equal(expected, body.Trim(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        [ReplaceCulture]
        public async Task OverrideAppWideDefaultsInViewAndPartialView()
        {
            // Arrange
            AppContext.TryGetSwitch(UseDateTimeLocalTypeForDateTimeOffsetSwitch, out var enabled);
            var expectedType = enabled ? "datetime-local" : "text";
            var expected =
$@"<div class=""validation-summary-errors""><ValidationSummaryInView>MySummary</ValidationSummaryInView>
<ul><li>A model error occurred.</li>
</ul></div>
<ValidationInView class=""field-validation-error"" data-valmsg-for=""Error"" data-valmsg-replace=""true"">An error occurred.</ValidationInView>
<input id=""Prefix!Property1"" name=""Prefix.Property1"" type=""text"" value="""" />
<div class=""editor-label""><label for=""MyDate"">MyDate</label></div>
<div class=""editor-field""><input class=""text-box single-line"" data-val=""true"" data-val-required=""The MyDate field is required."" id=""MyDate"" name=""MyDate"" type=""{expectedType}"" value=""02/01/2000 03:04:05 &#x2B;00:00"" /> <ValidationInView class=""field-validation-valid"" data-valmsg-for=""MyDate"" data-valmsg-replace=""true""></ValidationInView></div>

True
<div class=""validation-summary-errors""><ValidationSummaryInPartialView>MySummary</ValidationSummaryInPartialView>
<ul><li>A model error occurred.</li>
</ul></div>
<ValidationInPartialView class=""field-validation-error"" data-valmsg-for=""Error"" data-valmsg-replace=""true"">An error occurred.</ValidationInPartialView>
<input id=""Prefix!Property1"" name=""Prefix.Property1"" type=""text"" value="""" />
<div class=""editor-label""><label for=""MyDate"">MyDate</label></div>
<div class=""editor-field""><input class=""text-box single-line"" id=""MyDate"" name=""MyDate"" type=""{expectedType}"" value=""02/01/2000 03:04:05 &#x2B;00:00"" /> <ValidationInPartialView class=""field-validation-valid"" data-valmsg-for=""MyDate"" data-valmsg-replace=""true""></ValidationInPartialView></div>

True";

            // Act
            var body = await Client.GetStringAsync("http://localhost/HtmlHelperOptions/OverrideAppWideDefaultsInView");

            // Assert
            Assert.Equal(expected, body.Trim(), ignoreLineEndingDifferences: true);
        }
    }
}
