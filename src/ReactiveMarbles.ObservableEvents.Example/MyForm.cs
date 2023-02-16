// Copyright (c) 2019-2021 ReactiveUI Association Incorporated. All rights reserved.
// ReactiveUI Association Incorporated licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Threading.Tasks;

using ReactiveMarbles.ObservableEvents;

using Xamarin.Forms;

[assembly: GenerateStaticEventObservables(typeof(StaticTest))]

#pragma warning disable
namespace ReactiveMarbles.ObservableEvents
{
    /// <summary>
    /// Code behind for the same page.
    /// </summary>
    public partial class MyForm : Page
    {
        /// <summary>
        /// Test command.
        /// </summary>
        public void Test()
        {
            this.PropertyChangedRx().Subscribe();

            ////RxStaticTestEvents.TestChanged.Subscribe(); 
        }
    }

    public class MyNoEventsClass
    {
        public MyNoEventsClass()
        {
        }
    }

    public class AsyncEventsClass
    {
        public event Func<int, Task> TestEvent1;
        public event Func<int, ValueTask> TestEvent2;

        public int test => 1;

        public AsyncEventsClass()
        {
            var e1 = this.TestEvent1Rx();
            var e2 = this.TestEvent2Rx();
        }
    }

    public static class StaticTest
    {
        public static event EventHandler? TestChanged;
    }

    public class GenericTestClass<TGenericType, TGen2>
    {
        public event EventHandler<TGenericType> TestChanged;

        public GenericTestClass()
        {
            var events = this.TestChangedRx();
        }
    }

    public class SecondGenericTextClass<TGen3> : GenericTestClass<AsyncEventsClass, TGen3>
    {
        public event EventHandler TestChanged2;

        public SecondGenericTextClass()
        {
            var events = this.TestChangedRx();
        }
    }

    public class GenericClassesTestClass
    {
        public GenericClassesTestClass()
        {
            var colInt = new ObservableCollection<int>();

            var col = new ObservableCollection<AsyncEventsClass>();
        }
    }

    public class GenericConstraintsTestClass<TGen, TGen2, TGen3>
        where TGen : AsyncEventsClass
        where TGen2 : SecondGenericTextClass<TGen>
        where TGen3 : class, new()
    {
        public event EventHandler<TGen2> TestChanged;

        public GenericConstraintsTestClass()
        {
            var events = this.TestChangedRx();
        }
    }

    public class BasicTest
    {
        public event Action Test;

        public void test()
        {
            this.TestRx();
        }
    }
}
