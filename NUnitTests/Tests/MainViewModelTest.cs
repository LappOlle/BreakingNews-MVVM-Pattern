using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace NUnitTests.Tests
{
    [TestFixture]
    public class MainViewModelTest
    {
    
        private MainViewModel vmodel;

        [SetUp]
        public void Setup()
        {
            vmodel = new MainViewModel();

        }

        [Test]
        public async void RaisePropertyChange_Test()
        {
            int countInProgressRaised = 0;
            int expectedInProgressRaised = 2;

            int countNumberOfHitsRaised = 0;
            int expectedNumberOfHitsRaised = 1;

            int countProgressPercentRaised = 0;
            int expectedProgressPersentRaised = 100;

            List<string> RaisedPropertys = new List<string>();

            await Task.Run(() => vmodel.PropertyChanged += (sender, args) =>
            {
                RaisedPropertys.Add(args.PropertyName);
            });

            vmodel.GetStatButtonCommand.CanExecuteChanged += (sender, args) =>
            {

                foreach (var property in RaisedPropertys)
                {
                    switch (property)
                    {
                        case "InProgress":
                            {
                                countInProgressRaised++;
                                break;
                            }
                        case "ProgressPercent":
                            {
                                countProgressPercentRaised++;
                                break;
                            }
                        case "NumberOfHits":
                            countNumberOfHitsRaised++;
                            break;
                    }
                }
                Assert.AreEqual(expectedInProgressRaised, countInProgressRaised);
                Assert.AreEqual(expectedNumberOfHitsRaised, countNumberOfHitsRaised);
                Assert.AreEqual(expectedProgressPersentRaised, countProgressPercentRaised);
            };

            vmodel.GetStatButtonCommand.Execute(null);
        }
    }
}
