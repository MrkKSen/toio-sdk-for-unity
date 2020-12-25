﻿/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Assertions;
using System.IO;
using Vector=toio.MathUtils.Vector;
using static toio.MathUtils.Utils;
using toio.Navigation;

using test = toio.Tests.BasicTestMonoBehaviour;
using assert = UnityEngine.Assertions.Assert;

namespace toio.Tests
{
    /// <summary>
    /// 便利リンク
    /// 【Assertチートシート】
    /// https://qiita.com/su10/items/67a4a90c648b1ef68ab9#assertチートシート
    /// </summary>
    public class D_Navigator : CubeTestCase
    {
        [UnityTest, Order(0)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator a_ToTarget1()
        {
            var handle = new CubeHandle(test.CreateCube(150, 300, 90));
            var navigator = new CubeNavigator(handle, mode:Navigator.Mode.AVOID);

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    navigator.Update();
                    navigator.Navi2Target(250, 250, maxSpd:80).Exec();
                    last_time = now;
                }

                if (2.5 < Time.time - start_time){
                    Debug.LogFormat("handle.pos-target.pos = {0}", handle.pos-new Vector(250, 250));
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }

        [UnityTest, Order(0)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator b_BorderSingleIn()
        {
            var handle = new CubeHandle(test.CreateCube(150, 380, 90));
            var navigator = new CubeNavigator(handle, mode:Navigator.Mode.AVOID);

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    navigator.Update();

                    var res = navigator.Navi2Target(400, 500, 80).Exec();
                    last_time = now;
                }

                if (3 < Time.time - start_time){
                    Debug.LogFormat("handle.pos-target.pos = {0}", handle.pos-new Vector(400, 500));
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }


        [UnityTest, Order(0)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator b_BorderSingleOut()
        {
            var handle = new CubeHandle(test.CreateCube(150, 420, 135));
            var navigator = new CubeNavigator(handle, mode:Navigator.Mode.AVOID);

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    navigator.Update();

                    var res = navigator.Navi2Target(400, 500, 80).Exec();
                    last_time = now;
                }

                if (3 < Time.time - start_time){
                    Debug.LogFormat("handle.pos-target.pos = {0}", handle.pos-new Vector(400, 500));
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }

        [UnityTest, Order(1)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator c_Intersect1v1Lateral()
        {
            var handle0 = new CubeHandle(test.CreateCube(100, 100, 45));
            var handle1 = new CubeHandle(test.CreateCube(400, 100, 130));
            var navigator0 = new CubeNavigator(handle0, mode:Navigator.Mode.AVOID);
            var navigator1 = new CubeNavigator(handle1, mode:Navigator.Mode.AVOID);

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    navigator0.Update();navigator1.Update();
                    navigator0.Navi2Target(400, 400, maxSpd:80).Exec();
                    navigator1.Navi2Target(100, 400, maxSpd:80).Exec();
                    last_time = now;
                }

                if (4.5 < Time.time - start_time){
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }

        [UnityTest, Order(1)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator c_Intersect1v1Longitudinal()
        {
            var handle0 = new CubeHandle(test.CreateCube(100, 100, 45));
            var handle1 = new CubeHandle(test.CreateCube(400, 400, -135));
            var navigator0 = new CubeNavigator(handle0, mode:Navigator.Mode.AVOID);
            var navigator1 = new CubeNavigator(handle1, mode:Navigator.Mode.AVOID);

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    navigator0.Update();navigator1.Update();
                    navigator0.Navi2Target(400, 400, maxSpd:80).Exec();
                    navigator1.Navi2Target(100, 100, maxSpd:80).Exec();
                    last_time = now;
                }

                if (4 < Time.time - start_time){
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }

        [UnityTest, Order(2)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator d_ToTarget4()
        {
            List<CubeHandle> handles = new List<CubeHandle>();
            List<CubeNavigator> navigators = new List<CubeNavigator>();
            handles.Add(new CubeHandle(test.CreateCube(100, 100, 0)));
            handles.Add(new CubeHandle(test.CreateCube(100, 140, 45)));
            handles.Add(new CubeHandle(test.CreateCube(140, 100, 0)));
            handles.Add(new CubeHandle(test.CreateCube(150, 150, -90)));
            foreach (var handle in handles)
                navigators.Add(new CubeNavigator(handle, mode:Navigator.Mode.AVOID));
            foreach (var navi in navigators){
            }

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    foreach (var navi in navigators)
                        navi.Update(usePred:true);

                    foreach (var navi in navigators){
                        navi.Navi2Target(400, 400, maxSpd:80).Exec();
                    }

                    last_time = now;
                }

                if (4 < Time.time - start_time){
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }

        [UnityTest, Order(2)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator e_Intersect4v4Longitudinal()
        {
            List<CubeHandle> handles0 = new List<CubeHandle>();
            List<CubeNavigator> navigators0 = new List<CubeNavigator>();
            handles0.Add(new CubeHandle(test.CreateCube(100, 100, 0)));
            handles0.Add(new CubeHandle(test.CreateCube(100, 140, 45)));
            handles0.Add(new CubeHandle(test.CreateCube(140, 100, 0)));
            handles0.Add(new CubeHandle(test.CreateCube(150, 150, -90)));
            foreach (var handle in handles0)
                navigators0.Add(new CubeNavigator(handle, mode:Navigator.Mode.AVOID));

            List<CubeHandle> handles1 = new List<CubeHandle>();
            List<CubeNavigator> navigators1 = new List<CubeNavigator>();
            handles1.Add(new CubeHandle(test.CreateCube(400, 400, 180)));
            handles1.Add(new CubeHandle(test.CreateCube(400, 360, 135)));
            handles1.Add(new CubeHandle(test.CreateCube(360, 400, 90)));
            handles1.Add(new CubeHandle(test.CreateCube(350, 350, 90)));
            foreach (var handle in handles1)
                navigators1.Add(new CubeNavigator(handle, mode:Navigator.Mode.AVOID));

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    foreach (var navi in navigators0)
                        navi.Update(usePred:true);
                    foreach (var navi in navigators1)
                        navi.Update(usePred:true);

                    foreach (var navi in navigators0)
                        navi.Navi2Target(400, 400, maxSpd:80).Exec();
                    foreach (var navi in navigators1)
                        navi.Navi2Target(100, 100, maxSpd:80).Exec();

                    last_time = now;
                }

                if (6 < Time.time - start_time){
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }


        [UnityTest, Order(2)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator e_Intersect4v4Lateral()
        {
            List<CubeHandle> handles0 = new List<CubeHandle>();
            List<CubeNavigator> navigators0 = new List<CubeNavigator>();
            handles0.Add(new CubeHandle(test.CreateCube(100, 100, 0)));
            handles0.Add(new CubeHandle(test.CreateCube(100, 140, 45)));
            handles0.Add(new CubeHandle(test.CreateCube(140, 100, 0)));
            handles0.Add(new CubeHandle(test.CreateCube(150, 150, -90)));
            foreach (var handle in handles0)
                navigators0.Add(new CubeNavigator(handle, mode:Navigator.Mode.AVOID));

            List<CubeHandle> handles1 = new List<CubeHandle>();
            List<CubeNavigator> navigators1 = new List<CubeNavigator>();
            handles1.Add(new CubeHandle(test.CreateCube(400, 100, 90)));
            handles1.Add(new CubeHandle(test.CreateCube(400, 140, 45)));
            handles1.Add(new CubeHandle(test.CreateCube(360, 100, 0)));
            handles1.Add(new CubeHandle(test.CreateCube(350, 150, 0)));
            foreach (var handle in handles1)
                navigators1.Add(new CubeNavigator(handle, mode:Navigator.Mode.AVOID));

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    foreach (var navi in navigators0)
                        navi.Update(usePred:true);
                    foreach (var navi in navigators1)
                        navi.Update(usePred:true);

                    foreach (var navi in navigators0){
                        navi.Navi2Target(400, 400, maxSpd:80).Exec();
                    }
                    foreach (var navi in navigators1){
                        navi.Navi2Target(100, 400, maxSpd:80).Exec();
                    }

                    last_time = now;
                }

                if (7 < Time.time - start_time){
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }

        [UnityTest, Order(1)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator f_Rearend()
        {
            var handle0 = new CubeHandle(test.CreateCube(150, 250, 30));
            var handle1 = new CubeHandle(test.CreateCube(180, 250, 0));
            var navigator0 = new CubeNavigator(handle0, mode:Navigator.Mode.AVOID);
            var navigator1 = new CubeNavigator(handle1, mode:Navigator.Mode.AVOID);

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    navigator0.Update();navigator1.Update();

                    navigator0.Navi2Target(400, 150, maxSpd:80).Exec();
                    navigator1.Navi2Target(400, 250, maxSpd:80).Exec();

                    last_time = now;
                }

                if (3 < Time.time - start_time){
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }


        [UnityTest, Order(1)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator f_SideCollision()
        {
            var handle0 = new CubeHandle(test.CreateCube(150, 235, 10));
            var handle1 = new CubeHandle(test.CreateCube(150, 265, -10));
            var navigator0 = new CubeNavigator(handle0, mode:Navigator.Mode.AVOID);
            var navigator1 = new CubeNavigator(handle1, mode:Navigator.Mode.AVOID);

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    navigator0.Update();navigator1.Update();

                    navigator0.Navi2Target(400, 250, maxSpd:80).Exec();
                    navigator1.Navi2Target(400, 250, maxSpd:80).Exec();

                    last_time = now;
                }

                if (3 < Time.time - start_time){
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }

        [UnityTest, Order(1)] // テストの実行の優先度を指定する(昇順)
        public static IEnumerator g_AwayTarget()
        {
            var handle0 = new CubeHandle(test.CreateCube(150, 150, 0));
            var handle1 = new CubeHandle(test.CreateCube(250, 350, 0));
            var navigator0 = new CubeNavigator(handle0, mode:Navigator.Mode.AVOID);
            var navigator1 = new CubeNavigator(handle1, mode:Navigator.Mode.AVOID);
            navigator0.ClearOther();

            var start_time = Time.time;
            var last_time = start_time;

            test.init = (() =>
            {
            });

            test.update =  (() =>
            {
                var now = Time.time;

                if (now - last_time > 0.05)
                {
                    navigator0.Update();navigator1.Update();

                    navigator0.NaviAwayTarget(navigator1.handle.pos, maxSpd:80).Exec();
                    navigator1.Navi2Target(navigator0.handle.pos, maxSpd:40).Exec();

                    last_time = now;
                }

                if (6 < Time.time - start_time){
                    return true;
                }
                return false;
            });

            yield return new MonoBehaviourTest<test>();
        }

    }
}
*/