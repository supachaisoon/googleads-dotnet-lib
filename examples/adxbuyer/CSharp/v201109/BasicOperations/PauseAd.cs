// Copyright 2011, Google Inc. All Rights Reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

// Author: api.anash@gmail.com (Anash P. Oommen)

using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201109;

using System;
using System.Collections.Generic;
using System.IO;

namespace Google.Api.Ads.AdWords.Examples.CSharp.v201109 {
  /// <summary>
  /// This code example pauses a given ad. To list all ads, run GetTextAds.cs.
  ///
  /// Tags: AdGroupAdService.mutate
  /// </summary>
  class PauseAd : ExampleBase {
    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      ExampleBase codeExample = new PauseAd();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(new AdWordsUser(), codeExample.GetParameters(), Console.Out);
    }

    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This code example pauses a given ad. To list all ads, run GetTextAds.cs.";
      }
    }

    /// <summary>
    /// Gets the list of parameter names required to run this code example.
    /// </summary>
    /// <returns>
    /// A list of parameter names for this code example.
    /// </returns>
    public override string[] GetParameterNames() {
      return new string[] {"ADGROUP_ID", "AD_ID"};
    }

    /// <summary>
    /// Runs the code example.
    /// </summary>
    /// <param name="user">The AdWords user.</param>
    /// <param name="parameters">The parameters for running the code
    /// example.</param>
    /// <param name="writer">The stream writer to which script output should be
    /// written.</param>
    public override void Run(AdWordsUser user, Dictionary<string, string> parameters,
        TextWriter writer) {
      // Get the AdGroupAdService.
      AdGroupAdService service =
          (AdGroupAdService) user.GetService(AdWordsService.v201109.AdGroupAdService);

      long adGroupId = long.Parse(parameters["ADGROUP_ID"]);
      long adId = long.Parse(parameters["AD_ID"]);
      AdGroupAdStatus status = AdGroupAdStatus.PAUSED;

      // Create the ad group ad.
      AdGroupAd adGroupAd = new AdGroupAd();
      adGroupAd.status = status;
      adGroupAd.adGroupId = adGroupId;

      adGroupAd.ad = new Ad();
      adGroupAd.ad.id = adId;

      // Create the operation.
      AdGroupAdOperation adGroupAdOperation = new AdGroupAdOperation();
      adGroupAdOperation.@operator = Operator.SET;
      adGroupAdOperation.operand = adGroupAd;

      try {
        // Update the ad.
        AdGroupAdReturnValue retVal = service.mutate(new AdGroupAdOperation[]{adGroupAdOperation});

        // Display the results.
        if (retVal != null && retVal.value != null && retVal.value.Length > 0) {
          AdGroupAd pausedAdGroupAd = retVal.value[0];
          writer.WriteLine("Ad with id \"{0}\" was paused.", pausedAdGroupAd.ad.id);
        } else {
          writer.WriteLine("No ads were paused.");
        }
      } catch (Exception ex) {
        writer.WriteLine("Failed to pause ad. Exception says \"{0}\"", ex.Message);
      }
    }
  }
}
