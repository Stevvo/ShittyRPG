using GTANetworkServer;
using GTANetworkShared;
using System;
namespace IcaroRPG
{
    public class World : Script 
    {
        public World()
        {
            API.onResourceStart += onStart;
        }

        public void onStart()
        {
            API.setTime(18, 30);
            API.requestIpl("shr_int"); //simeons
            API.removeIpl("shutter_closed"); // simeons
            API.removeIpl("fakeint"); // simeons
            API.requestIpl("apa_v_mp_h_01_a");
            API.requestIpl("apa_v_mp_h_01_c");
            API.requestIpl("apa_v_mp_h_01_b");
            API.requestIpl("apa_v_mp_h_02_a");
            API.requestIpl("apa_v_mp_h_02_c");
            API.requestIpl("apa_v_mp_h_02_b");
            API.requestIpl("apa_v_mp_h_03_a");
            API.requestIpl("apa_v_mp_h_03_c");
            API.requestIpl("apa_v_mp_h_03_b");
            API.requestIpl("apa_v_mp_h_04_a");
            API.requestIpl("apa_v_mp_h_04_c");
            API.requestIpl("apa_v_mp_h_04_b");
            API.requestIpl("apa_v_mp_h_05_a");
            API.requestIpl("apa_v_mp_h_05_c");
            API.requestIpl("apa_v_mp_h_05_b");
            API.requestIpl("apa_v_mp_h_06_a");
            API.requestIpl("apa_v_mp_h_06_c");
            API.requestIpl("apa_v_mp_h_06_b");
            API.requestIpl("apa_v_mp_h_07_a");
            API.requestIpl("apa_v_mp_h_07_c");
            API.requestIpl("apa_v_mp_h_07_b");
            API.requestIpl("apa_v_mp_h_08_a");
            API.requestIpl("apa_v_mp_h_08_c");
            API.requestIpl("apa_v_mp_h_08_b");
            setRandomWeather();
            PopulateMap();

            API.delay(60000, false, () =>
            {
                API.setTime(API.getTime().Hours + (int)0.1, 5);
            });

            API.delay(1000000, false, () => { setRandomWeather(); });
        }

        public void setRandomWeather()
        {
            Random rnd = new Random();
            int weather = rnd.Next(0, 10);
            API.setWeather(weather);
        }

        public void PopulateMap()
        {
            //prison exterior start
            API.createObject(-151113999, new Vector3(1612.38184, 2523.67725, 44.5648766), new Vector3(-1.192091837, -1.192091837, 4.141859245));
            API.createObject(-151113999, new Vector3(1620.85962, 2579.76123, 44.5648766), new Vector3(-0, 0, 90.0000229));
            API.createObject(-1975652018, new Vector3(1612.62976, 2567.23145, 44.6985893), new Vector3(8.977776257, -1.01777744e-12, 43.6259537));
            API.createObject(-1975652018, new Vector3(1613.75, 2568.29443, 44.6985893), new Vector3(8.976180087, 1.692273888, 44.7060852));
            API.createObject(-1975652018, new Vector3(1614.81995, 2569.36475, 44.6985893), new Vector3(1.141378226, 8.39208369e-10, 43.6795731));
            API.createObject(-1975652018, new Vector3(1616.98083, 2571.521, 44.6985893), new Vector3(8.958321597, 5.909459948, 47.4001312));
            API.createObject(-1975652018, new Vector3(1617.99927, 2572.64038, 44.6985893), new Vector3(1.140347996, 4.303620938, 46.3736229));
            API.createObject(-1975652018, new Vector3(1615.91174, 2570.40649, 44.6985893), new Vector3(8.967870147, 4.219700148, 46.3199959));
            API.createObject(-1975652018, new Vector3(1611.56689, 2568.35059, 44.6985893), new Vector3(9.579935077, -8.958054427, -46.3513908));
            API.createObject(-1975652018, new Vector3(1610.49866, 2569.46948, 44.6985893), new Vector3(9.86175586e-10, -8.977773977, -46.373951));
            API.createObject(631614199, new Vector3(1618.13306, 2574.67065, 45.7148781), new Vector3(9.87091409e-10, 8.977777387, 133.625931));
            API.createObject(-1975652018, new Vector3(1618.16394, 2574.67944, 44.6985893), new Vector3(9.85717508e-10, 8.977777387, 133.626022));
            API.createObject(1042000049, new Vector3(1618.99756, 2573.66626, 46.9056664), new Vector3(9.86837057e-10, 8.977777957, 133.625946));
            API.createObject(-1975652018, new Vector3(1617.0946, 2575.79639, 44.6985893), new Vector3(9.86124515e-10, 8.977777957, 133.625992));
            API.createObject(-1975652018, new Vector3(1609.43372, 2570.58765, 44.6985893), new Vector3(9.84852422e-10, -8.977773977, -46.3740349));
            API.createObject(-1975652018, new Vector3(1615.30493, 2530.53052, 44.6985893), new Vector3(9.84954118e-10, -8.977772277, 41.112339));
            API.createObject(-1975652018, new Vector3(1618.00378, 2535.3335, 44.6985893), new Vector3(-3.331572888, -6.29169328e-10, 134.118454));
            API.createObject(-1975652018, new Vector3(1616.94446, 2536.41431, 44.6985893), new Vector3(2.102792547, -3.22062967e-11, 133.091949));
            API.createObject(-1975652018, new Vector3(1615.91406, 2537.51685, 44.6985893), new Vector3(-3.328326068, -1.567173729, 135.732376));
            API.createObject(-1975652018, new Vector3(1617.92517, 2533.15112, 44.6985893), new Vector3(9.576253897, 3.529377328, 43.0609932));
            API.createObject(-1975652018, new Vector3(1616.79541, 2532.09424, 44.6985893), new Vector3(9.84691773e-10, 3.332181828, 43.0384293));
            API.createObject(631614199, new Vector3(1611.67285, 2539.78174, 45.7148781), new Vector3(9.86789317e-10, -3.332151988, -136.96167));
            API.createObject(-1975652018, new Vector3(1611.66431, 2539.81274, 44.6985893), new Vector3(9.86840276e-10, -3.332151988, -136.961578));
            API.createObject(1042000049, new Vector3(1612.68616, 2540.63574, 46.9056664), new Vector3(9.86797311e-10, -3.332151988, -136.961655));
            API.createObject(-1975652018, new Vector3(1610.53638, 2538.75488, 44.6985893), new Vector3(9.86806747e-10, -3.332151988, -136.961609));
            API.createObject(-1975652018, new Vector3(1615.66626, 2531.04102, 44.6985893), new Vector3(9.84718862e-10, 3.332181828, 43.0383453));
            API.createObject(-1975652018, new Vector3(1619.05518, 2534.20239, 44.6985893), new Vector3(-3.33216698, -1.03049969e-12, 133.03833));
            API.createObject(-1975652018, new Vector3(1624.62195, 2511.72827, 44.6985893), new Vector3(8.976179527, 1.692310118, 95.3308792));
            API.createObject(-1975652018, new Vector3(1624.47339, 2513.23438, 44.6985893), new Vector3(1.141377886, 8.39526448e-10, 94.3043671));
            API.createObject(-1975652018, new Vector3(1624.35107, 2514.77197, 44.7004662), new Vector3(1.529729736, 4.219697668, 96.3138275));
            API.createObject(-1975652018, new Vector3(1623.19348, 2510.07642, 44.6985893), new Vector3(9.579937347, -8.958051587, 4.27340269));
            API.createObject(-1975652018, new Vector3(1621.651, 2509.96045, 44.6985893), new Vector3(9.86506321e-10, -8.977771137, 4.25083828));
            API.createObject(631614199, new Vector3(1622.47363, 2519.16138, 45.7148781), new Vector3(9.86741688e-10, 8.977776257, -175.749252));
            API.createObject(-1975652018, new Vector3(1622.48645, 2519.19092, 44.6985893), new Vector3(9.85348581e-10, 8.977775117, -175.749176));
            API.createObject(1042000049, new Vector3(1623.79846, 2519.19238, 46.9056664), new Vector3(9.86525528e-10, 8.977776257, -175.749237));
            API.createObject(-1975652018, new Vector3(1620.94458, 2519.073, 44.6985893), new Vector3(9.85755699e-10, 8.977775117, -175.749207));
            API.createObject(-1975652018, new Vector3(1620.11096, 2509.84668, 44.6985893), new Vector3(9.85202364e-10, -8.977772277, 4.25075674));
            API.createObject(-1975652018, new Vector3(1624.73291, 2510.18799, 44.6985893), new Vector3(8.977773977, -6.48833092e-13, 94.2507477));
            API.createObject(-1975652018, new Vector3(1652.58728, 2493.87891, 44.6985893), new Vector3(8.976129497, 1.692282768, -177.19957));
            API.createObject(-1975652018, new Vector3(1651.07617, 2493.79688, 44.6985893), new Vector3(1.141372996, 8.39341985e-10, -178.226089));
            API.createObject(-1975652018, new Vector3(1649.56775, 2493.75073, 44.6985893), new Vector3(8.967817287, 4.219691268, -175.585663));
            API.createObject(-1975652018, new Vector3(1654.17456, 2492.37866, 44.6985893), new Vector3(9.579937347, -8.95800277, 91.7429352));
            API.createObject(-1975652018, new Vector3(1654.22229, 2490.83276, 44.6985893), new Vector3(9.86325022e-10, -8.977723387, 91.7203751));
            API.createObject(631614199, new Vector3(1645.06653, 2492.06079, 45.7148781), new Vector3(9.86929316e-10, 8.977726227, -88.2797241));
            API.createObject(-1975652018, new Vector3(1645.03772, 2492.07471, 44.6985893), new Vector3(9.85533211e-10, 8.977727367, -88.2796402));
            API.createObject(1042000049, new Vector3(1645.09399, 2493.38574, 46.9056664), new Vector3(9.86713156e-10, 8.977726227, -88.2797089));
            API.createObject(-1975652018, new Vector3(1645.0874, 2490.5293, 44.6985893), new Vector3(9.85949655e-10, 8.977726227, -88.2796631));
            API.createObject(-1975652018, new Vector3(1654.26794, 2489.28906, 44.6985893), new Vector3(9.8501951e-10, -8.977723387, 91.7202911));
            API.createObject(-1975652018, new Vector3(1654.13098, 2493.92163, 44.6985893), new Vector3(8.977725097, -8.3966639e-13, -178.279709));
            API.createObject(-1975652018, new Vector3(1678.57263, 2484.6355, 44.6986618), new Vector3(8.976168157, 1.692192348, 135.655914));
            API.createObject(-1975652018, new Vector3(1677.48474, 2485.6875, 44.6986618), new Vector3(1.141376746, 8.38343284e-10, 134.629395));
            API.createObject(-1975652018, new Vector3(1676.42517, 2486.76196, 44.6986618), new Vector3(8.967857077, 4.219618798, 137.269821));
            API.createObject(-1975652018, new Vector3(1678.55261, 2482.45166, 44.6986618), new Vector3(9.579925977, -8.958041357, 44.5984421));
            API.createObject(-1975652018, new Vector3(1677.45166, 2481.36499, 44.6986618), new Vector3(9.85361348e-10, -8.977762047, 44.5758781));
            API.createObject(631614199, new Vector3(1672.12476, 2488.91211, 45.7149506), new Vector3(9.87905646e-10, 8.977766027, -135.42421));
            API.createObject(-1975652018, new Vector3(1672.11536, 2488.94287, 44.6986618), new Vector3(9.86531745e-10, 8.977763747, -135.424133));
            API.createObject(1042000049, new Vector3(1673.11462, 2489.79321, 46.9057388), new Vector3(9.87702253e-10, 8.977764887, -135.42421));
            API.createObject(-1975652018, new Vector3(1671.01611, 2487.85522, 44.6986618), new Vector3(9.86938753e-10, 8.977764887, -135.424164));
            API.createObject(-1975652018, new Vector3(1676.3512, 2480.28174, 44.6986618), new Vector3(9.83987336e-10, -8.977762047, 44.5757942));
            API.createObject(-1975652018, new Vector3(1679.65405, 2483.53296, 44.6986618), new Vector3(8.977763177, -1.78111052e-12, 134.57579));
            API.createObject(-1975652018, new Vector3(1711.79797, 2489.2168, 44.698658), new Vector3(8.976170997, 1.69225868, -136.812759));
            API.createObject(-1975652018, new Vector3(1710.70007, 2488.17529, 44.698658), new Vector3(1.14137726, 8.3900481e-10, -137.839264));
            API.createObject(-1975652018, new Vector3(1709.58105, 2487.16284, 44.698658), new Vector3(8.967859917, 4.219684878, -135.198837));
            API.createObject(-1975652018, new Vector3(1713.97888, 2489.10278, 44.698658), new Vector3(9.579932797, -8.958045337, 132.129761));
            API.createObject(-1975652018, new Vector3(1715.01709, 2487.95605, 44.698658), new Vector3(9.8602293e-10, -8.977766027, 132.107193));
            API.createObject(631614199, new Vector3(1707.24756, 2482.95923, 45.7149467), new Vector3(9.87193216e-10, 8.977767727, -47.8929024));
            API.createObject(-1975652018, new Vector3(1707.21643, 2482.95117, 44.698658), new Vector3(9.85768467e-10, 8.977768867, -47.8928108));
            API.createObject(1042000049, new Vector3(1706.41003, 2483.98608, 46.905735), new Vector3(9.86938864e-10, 8.977767727, -47.8928871));
            API.createObject(-1975652018, new Vector3(1708.25574, 2481.80615, 44.698658), new Vector3(9.86226323e-10, 8.977767727, -47.8928413));
            API.createObject(-1975652018, new Vector3(1716.052, 2486.81006, 44.698658), new Vector3(9.84699766e-10, -8.977764887, 132.107101));
            API.createObject(-1975652018, new Vector3(1712.94604, 2490.24976, 44.698658), new Vector3(8.977767157, -1.11955519e-12, -137.892883));
            API.createObject(-1975652018, new Vector3(1734.33643, 2507.87036, 44.6987152), new Vector3(1.819310116, 3.430072948, 165.219772));
            API.createObject(-1975652018, new Vector3(1732.87109, 2508.24854, 44.6987152), new Vector3(2.063233756, 1.702283659, 164.193253));
            API.createObject(-1975652018, new Vector3(1731.41931, 2508.66016, 44.6987152), new Vector3(1.817623916, 8.552699178, 166.833679));
            API.createObject(-1975652018, new Vector3(1735.39636, 2505.96094, 44.6987152), new Vector3(9.583583287, -1.817661316, 74.1623001));
            API.createObject(-1975652018, new Vector3(1734.97498, 2504.47266, 44.6987152), new Vector3(9.87905757e-10, -1.819633556, 74.1397324));
            API.createObject(631614199, new Vector3(1726.6178, 2508.40869, 45.715004), new Vector3(9.87142368e-10, 1.819633556, -105.860367));
            API.createObject(-1975652018, new Vector3(1726.59448, 2508.43091, 44.6987152), new Vector3(9.84343607e-10, 1.819633556, -105.860275));
            API.createObject(1042000049, new Vector3(1727.04419, 2509.66357, 46.9057922), new Vector3(9.86684401e-10, 1.819633556, -105.860352));
            API.createObject(-1975652018, new Vector3(1726.17505, 2506.94238, 44.6987152), new Vector3(9.85106774e-10, 1.819633556, -105.860306));
            API.createObject(-1975652018, new Vector3(1734.55225, 2502.9873, 44.6987152), new Vector3(9.85259541e-10, -1.819633446, 74.1396484));
            API.createObject(-1975652018, new Vector3(1735.82092, 2507.44482, 44.6987152), new Vector3(1.819633556, -7.1244421e-13, 164.139648));
            API.createObject(-1975652018, new Vector3(1760.68909, 2528.28955, 44.6987152), new Vector3(8.976168157, 1.692350088, -106.691071));
            API.createObject(-1975652018, new Vector3(1760.26221, 2526.83765, 44.6987152), new Vector3(1.141376866, 8.39946279e-10, -107.71759));
            API.createObject(-1975652018, new Vector3(1759.80237, 2525.40039, 44.6987152), new Vector3(8.96785487, 4.219766238, -105.077164));
            API.createObject(-1975652018, new Vector3(1762.63281, 2529.28516, 44.6987152), new Vector3(9.579943037, -8.958041357, 162.251434));
            API.createObject(-1975652018, new Vector3(1764.1062, 2528.81445, 44.6987152), new Vector3(9.86964288e-10, -8.97776097, 162.228867));
            API.createObject(631614199, new Vector3(1759.89355, 2520.59326, 45.715004), new Vector3(9.86353665e-10, 8.977764887, -17.7712307));
            API.createObject(-1975652018, new Vector3(1759.87061, 2520.57056, 44.6987152), new Vector3(9.85005189e-10, 8.977764887, -17.771143));
            API.createObject(1042000049, new Vector3(1758.65369, 2521.06128, 46.9057922), new Vector3(9.86175697e-10, 8.977764887, -17.7712193));
            API.createObject(-1975652018, new Vector3(1761.34424, 2520.10181, 44.6987152), new Vector3(9.85386661e-10, 8.977764887, -17.7711697));
            API.createObject(-1975652018, new Vector3(1765.57654, 2528.34229, 44.6987152), new Vector3(9.85615811e-10, -8.977759767, 162.22879));
            API.createObject(-1975652018, new Vector3(1761.16382, 2529.75903, 44.6987152), new Vector3(8.977763177, -2.54444361e-13, -107.77121));
            API.createObject(-1975652018, new Vector3(1736.89612, 2560.51904, 44.6985893), new Vector3(8.976164177, 1.692186128, -0.866545439));
            API.createObject(-1975652018, new Vector3(1738.4093, 2560.50415, 44.6985893), new Vector3(1.14137646, 8.3831786e-10, -1.89305615));
            API.createObject(-1975652018, new Vector3(1739.94763, 2560.45361, 44.6985893), new Vector3(8.967853097, 4.219604938, 0.747369587));
            API.createObject(-1975652018, new Vector3(1735.40808, 2562.11743, 44.6985893), new Vector3(9.579927117, -8.958036237, -91.9240189));
            API.createObject(-1975652018, new Vector3(1735.45923, 2563.66333, 44.6985893), new Vector3(9.85300952e-10, -8.977756927, -91.946579));
            API.createObject(631614199, new Vector3(1744.5177, 2561.85229, 45.7148781), new Vector3(9.87956605e-10, 8.977758637, 88.053299));
            API.createObject(-1975652018, new Vector3(1744.54553, 2561.83643, 44.6985893), new Vector3(9.86560389e-10, 8.977758637, 88.0533905));
            API.createObject(1042000049, new Vector3(1744.40552, 2560.53174, 46.9056664), new Vector3(9.87740334e-10, 8.977758637, 88.0533142));
            API.createObject(-1975652018, new Vector3(1744.59485, 2563.38208, 44.6985893), new Vector3(9.86977056e-10, 8.977759767, 88.05336));
            API.createObject(-1975652018, new Vector3(1735.51245, 2565.20679, 44.6985893), new Vector3(9.83990556e-10, -8.977758067, -91.9466629));
            API.createObject(-1975652018, new Vector3(1735.35278, 2560.57495, 44.6985893), new Vector3(8.977758067, -1.86698552e-12, -1.94667661));
            API.createObject(-1975652018, new Vector3(1674.07227, 2562.66064, 44.6985893), new Vector3(1.783235466, 3.361919458, 0.0529357828));
            API.createObject(-1975652018, new Vector3(1675.58569, 2562.67017, 44.6985893), new Vector3(2.02715256, 1.66707179, -0.973574996));
            API.createObject(-1975652018, new Vector3(1677.09448, 2562.6438, 44.6985893), new Vector3(1.78158286, 8.382968998, 1.66685069));
            API.createObject(-1975652018, new Vector3(1672.55884, 2564.23511, 44.6985893), new Vector3(9.583427537, -1.781580296, -91.0045395));
            API.createObject(-1975652018, new Vector3(1672.58521, 2565.78174, 44.6985893), new Vector3(9.86471349e-10, -1.783552196, -91.0270996));
            API.createObject(631614199, new Vector3(1681.67151, 2564.11621, 45.7148781), new Vector3(9.88478188e-10, 1.783552536, 88.9727783));
            API.createObject(-1975652018, new Vector3(1681.69971, 2564.10083, 44.6985893), new Vector3(9.8571451e-10, 1.783552766, 88.9728699));
            API.createObject(1042000049, new Vector3(1681.58057, 2562.79395, 46.9056664), new Vector3(9.88052085e-10, 1.783552536, 88.9727936));
            API.createObject(-1975652018, new Vector3(1681.72412, 2565.64697, 44.6985893), new Vector3(9.86547621e-10, 1.783552766, 88.9728394));
            API.createObject(-1975652018, new Vector3(1672.61365, 2567.32568, 44.6985893), new Vector3(9.83898296e-10, -1.783551976, -91.0271835));
            API.createObject(-1975652018, new Vector3(1672.52832, 2562.69189, 44.6985893), new Vector3(1.783552426, -2.06736039e-12, -1.02719545));
            API.createObject(-1975652018, new Vector3(1701.31848, 2562.82007, 44.6985893), new Vector3(8.976017517, 1.692261978, -0.572780728));
            API.createObject(-1975652018, new Vector3(1702.83179, 2562.81299, 44.6985893), new Vector3(1.141361746, 8.39335601e-10, -1.59929156));
            API.createObject(-1975652018, new Vector3(1704.34021, 2562.77026, 44.6985893), new Vector3(8.967706447, 4.219639048, 1.04113436));
            API.createObject(-1975652018, new Vector3(1699.82227, 2564.41089, 44.6985893), new Vector3(9.579937347, -8.957891857, -91.6302567));
            API.createObject(-1975652018, new Vector3(1699.86548, 2565.95728, 44.6985893), new Vector3(9.86329796e-10, -8.97761147, -91.6528244));
            API.createObject(631614199, new Vector3(1708.93311, 2564.19238, 45.7148781), new Vector3(9.86929205e-10, 8.977614247, 88.3470612));
            API.createObject(-1975652018, new Vector3(1708.96118, 2564.17676, 44.6985893), new Vector3(9.85536208e-10, 8.977614247, 88.3471527));
            API.createObject(1042000049, new Vector3(1708.82776, 2562.87134, 46.9056664), new Vector3(9.8671471e-10, 8.977614247, 88.3470764));
            API.createObject(-1975652018, new Vector3(1709.00244, 2565.72266, 44.6985893), new Vector3(9.85946547e-10, 8.977614247, 88.3471298));
            API.createObject(-1975652018, new Vector3(1699.91077, 2567.50073, 44.6985893), new Vector3(9.85022619e-10, -8.977610267, -91.6529007));
            API.createObject(-1975652018, new Vector3(1699.7749, 2562.86816, 44.6985893), new Vector3(8.977612547, -8.34895575e-13, -1.6529119));
            API.createObject(-1975652018, new Vector3(1678.64404, 2562.69385, 44.6985893), new Vector3(1.78158286, 8.382968998, 1.66685069));
            API.createObject(-1975652018, new Vector3(1680.17554, 2562.73389, 44.6985893), new Vector3(1.78158286, 8.382968998, 1.66685057));
            API.createObject(-1975652018, new Vector3(1705.88171, 2562.80029, 44.6985893), new Vector3(8.967706447, 4.219639048, 1.04113436));
            API.createObject(-1975652018, new Vector3(1707.42322, 2562.83032, 44.6985893), new Vector3(8.967706447, 4.219639048, 1.04113436));
            API.createObject(-1975652018, new Vector3(1741.48914, 2560.47363, 44.6985893), new Vector3(8.967853667, 4.219604938, 0.747369528));
            API.createObject(-1975652018, new Vector3(1743.04065, 2560.50366, 44.6985893), new Vector3(8.967853667, 4.219604938, 0.747369528));
            API.createObject(-1975652018, new Vector3(1759.39746, 2523.90503, 44.6987152), new Vector3(2.00409596, 4.219623768, -106.260468));
            API.createObject(-1975652018, new Vector3(1758.96643, 2522.42407, 44.7037086), new Vector3(1.766449266, 4.219639048, -105.226227));
            API.createObject(-1975652018, new Vector3(1729.92468, 2509.03564, 44.7103119), new Vector3(1.612494887, 8.552375168, 164.625397));
            API.createObject(-1975652018, new Vector3(1728.44641, 2509.46802, 44.7303391), new Vector3(-1.022275247, 8.552363798, 168.808517));
            API.createObject(-1975652018, new Vector3(1708.49292, 2486.06885, 44.698658), new Vector3(1.844822016, 4.219786838, -136.447861));
            API.createObject(-1975652018, new Vector3(1707.34436, 2485.02222, 44.698658), new Vector3(3.316059066, 4.21904368, -130.94075));
            API.createObject(-1975652018, new Vector3(1675.31653, 2487.82007, 44.721962), new Vector3(1.806477266, 4.219684878, 140.470749));
            API.createObject(-1975652018, new Vector3(1674.15088, 2488.79248, 44.698658), new Vector3(1.806475216, 4.219857898, 139.144394));
            API.createObject(-1975652018, new Vector3(1648.03015, 2493.65698, 44.7290688), new Vector3(8.967804217, 4.219655748, -173.623474));
            API.createObject(-1975652018, new Vector3(1646.50134, 2493.44165, 44.6985893), new Vector3(8.967790567, 4.219644728, -176.213577));
            API.createObject(-1975652018, new Vector3(1624.19714, 2516.30933, 44.6985893), new Vector3(3.494601976, 4.219664628, 100.569786));
            API.createObject(-1975652018, new Vector3(1623.93921, 2517.82861, 44.7005081), new Vector3(2.599161466, 4.219621278, 97.0342484));
            API.createObject(-1975652018, new Vector3(1614.80383, 2538.58862, 44.6985893), new Vector3(-3.328326418, -1.567143539, 134.067566));
            API.createObject(-1975652018, new Vector3(1613.70483, 2539.71631, 44.6985893), new Vector3(-3.328326418, -1.567119669, 135.22847));
            // prison interior start
            API.createObject(251770068, new Vector3(1726.73511, 2567.65015, 45.1684265), new Vector3(0, 0, 8.26446746));
            API.createObject(251770068, new Vector3(1726.73511, 2567.65015, 48.9683685), new Vector3(0, 0, 8.264466496));
            API.createObject(251770068, new Vector3(1726.73511, 2572.65503, 48.9683685), new Vector3(0, 0, 8.264465586));
            API.createObject(251770068, new Vector3(1726.73511, 2572.65503, 45.1684265), new Vector3(0, 0, 8.264466496));
            API.createObject(251770068, new Vector3(1726.73511, 2577.65991, 48.9683685), new Vector3(0, 0, 8.264465586));
            API.createObject(251770068, new Vector3(1726.73511, 2577.65991, 45.1684265), new Vector3(0, 0, 8.264465586));
            API.createObject(251770068, new Vector3(1731.23401, 2579.06128, 48.9683685), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1731.23401, 2579.06128, 45.1684265), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1736.23279, 2579.06128, 45.1684265), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1736.23279, 2579.06128, 48.9684296), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1741.23157, 2579.06128, 48.9684296), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1741.23157, 2579.06128, 45.1684875), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1746.23035, 2579.06128, 45.1684875), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1746.23035, 2579.06128, 48.9684296), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1751.22913, 2579.06128, 48.9684296), new Vector3(-0, 0, 90));
            API.createObject(251770068, new Vector3(1751.22913, 2579.06128, 45.1684875), new Vector3(-0, 0, 90));
            API.createObject(1857662402, new Vector3(1728.74585, 2568.43188, 50.8817749), new Vector3(0, 0, 89.9999619));
            API.createObject(1857662402, new Vector3(1728.74585, 2574.14746, 50.8817749), new Vector3(0, 0, 89.9999542));
            API.createObject(1857662402, new Vector3(1731.56531, 2577.11035, 50.8817749), new Vector3(-0, 0, -179.999954));
            API.createObject(1857662402, new Vector3(1737.28381, 2577.11035, 50.8817749), new Vector3(-0, 0, -179.999939));
            API.createObject(1857662402, new Vector3(1743.00232, 2577.11035, 50.8817749), new Vector3(-0, 0, -179.999924));
            API.createObject(1857662402, new Vector3(1745.86169, 2577.11035, 50.8817749), new Vector3(-0, 0, -179.999908));
            API.createObject(-923555145, new Vector3(1736.07996, 2568.94482, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(-923555145, new Vector3(1736.07996, 2570.40625, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(-923555145, new Vector3(1736.07996, 2571.86768, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(-923555145, new Vector3(1736.07996, 2573.3291, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(1427480666, new Vector3(1734.61584, 2569.33643, 44.8003769), new Vector3(0, 0, 89.9813004));
            API.createObject(1427480666, new Vector3(1734.63367, 2573.16846, 44.8003769), new Vector3(-0, 0, -90.6982574));
            API.createObject(1427480666, new Vector3(1737.44678, 2573.16846, 44.8005028), new Vector3(4.442113667, -6.95746298e-16, 89.8358917));
            API.createObject(1427480666, new Vector3(1737.37341, 2569.20532, 44.8005028), new Vector3(-3.396636319, -5.43551796e-18, 90.2191467));
            API.createObject(-923555145, new Vector3(1741.77856, 2573.3291, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(-923555145, new Vector3(1741.77856, 2571.86963, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(-923555145, new Vector3(1741.77856, 2571.86963, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(-923555145, new Vector3(1741.77856, 2570.4082, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(-923555145, new Vector3(1741.77856, 2568.95679, 44.5696869), new Vector3(-0, 0, 90.0000076));
            API.createObject(1427480666, new Vector3(1740.43677, 2573.16846, 44.8005028), new Vector3(4.442113667, -6.95746298e-16, 89.8358917));
            API.createObject(1427480666, new Vector3(1743.13611, 2573.16846, 44.8005028), new Vector3(4.442113667, -6.95746298e-16, 89.8358917));
            API.createObject(1427480666, new Vector3(1740.40271, 2569.20532, 44.8005028), new Vector3(-3.396636319, -5.43551796e-18, 90.2191467));
            API.createObject(1427480666, new Vector3(1743.14209, 2569.20532, 44.8005028), new Vector3(-3.396636319, -5.43551796e-18, 90.2191467));
            API.createObject(-1188733122, new Vector3(1744.18555, 2576.70044, 44.9135666), new Vector3(2.17315637, -3.72721231e-16, 0.0732205808));
            API.createObject(-1188733122, new Vector3(1737.92297, 2576.71704, 44.9135666), new Vector3(-7.382367937, 1.9083327e-14, 1.19218731));
            API.createObject(-1610620247, new Vector3(1728.78015, 2575.5459, 45.7491913), new Vector3(0, 0, 89.2157516));
            API.createObject(-1567006928, new Vector3(1731.89795, 2575.83105, 44.5651321), new Vector3(0, 0, 89.9999466));
            API.createObject(-1567006928, new Vector3(1731.89795, 2573.34863, 44.5651321), new Vector3(0, 0, 89.9999466));
            API.createObject(-1567006928, new Vector3(1731.89795, 2570.86621, 44.5651321), new Vector3(0, 0, 89.9999466));
            API.createObject(-1567006928, new Vector3(1731.89795, 2568.38379, 44.5651321), new Vector3(0, 0, 89.9999466));
            API.createObject(-1326449699, new Vector3(1730.13574, 2567.56982, 44.5651321), new Vector3(0, 0, -4.606269616));
            API.createObject(-483631019, new Vector3(1729.19507, 2568.83545, 44.5251389), new Vector3(-0, 0, 90.0000153));
            API.createObject(-483631019, new Vector3(1729.19507, 2570.55713, 44.5251389), new Vector3(-0, 0, 90.0000076));
            API.createObject(-483631019, new Vector3(1729.19507, 2572.27881, 44.5251389), new Vector3(-0, 0, 90.0000076));
            API.createObject(-1668478519, new Vector3(1735.62842, 2573.81714, 45.3818359), new Vector3(-0, 0, -90.2367554));
            API.createObject(-1668478519, new Vector3(1735.60364, 2572.63965, 45.3818359), new Vector3(0, 0, -86.7946472));
            API.createObject(-1668478519, new Vector3(1736.51147, 2573.35767, 45.3818359), new Vector3(0, 0, 87.7203293));
            API.createObject(-1668478519, new Vector3(1736.50781, 2568.68384, 45.3818359), new Vector3(-0, 0, 102.393646));
            API.createObject(-1668478519, new Vector3(1741.65149, 2572.52075, 45.3818359), new Vector3(0, 0, 82.6406021));
            API.createObject(-1668478519, new Vector3(1742.18982, 2573.89429, 45.3818359), new Vector3(-0, 0, 91.4165955));
            API.createObject(-1668478519, new Vector3(1741.43494, 2569.83472, 45.3818359), new Vector3(0, 0, 78.4446182));
            API.createObject(-1668478519, new Vector3(1742.23889, 2568.89233, 45.3818359), new Vector3(0, 0, 84.6838531));
            API.createObject(-1668478519, new Vector3(1736.52222, 2569.81958, 45.3818359), new Vector3(0, 0, 86.8848953));
            API.createObject(916514878, new Vector3(1731.93579, 2576.23145, 45.4857788), new Vector3(-0, 0, 90.0237503));
            API.createObject(916514878, new Vector3(1729.66699, 2567.49365, 45.0650024), new Vector3(0, 0, -0.0920139104));
            API.createObject(916514878, new Vector3(1730.70288, 2567.50366, 45.0650024), new Vector3(0, 0, 0.0804876313));
            API.createObject(916514878, new Vector3(1731.91516, 2567.8103, 44.7352905), new Vector3(0, 0, 74.3173065));
            API.createObject(-1030226139, new Vector3(1729.26489, 2571.75342, 45.5601463), new Vector3(0, 0, 89.2203217));
            API.createObject(-1591250544, new Vector3(1729.35071, 2572.52612, 45.5514259), new Vector3(0, 0, 69.4063721));
            API.createObject(-920794651, new Vector3(1729.1781, 2570.39551, 45.5464249), new Vector3(0, 0, -83.4938431));
            API.createObject(-1610620247, new Vector3(1749.05432, 2568.18628, 45.813652), new Vector3(-0, 0, -90.2768555));
            API.createObject(-1188733122, new Vector3(1737.64441, 2565.43677, 44.9135666), new Vector3(-0, 0, 174.932739));
            API.createObject(-1188733122, new Vector3(1743.74158, 2565.37744, 44.9038696), new Vector3(-0, 0, 179.494995));
            API.createObject(916514878, new Vector3(1731.93372, 2575.19873, 45.4857788), new Vector3(0, 0, 89.9601059));
            API.createObject(916514878, new Vector3(1731.9436, 2573.03418, 45.4857788), new Vector3(0, 0, 83.5398788));
            API.createObject(-78626473, new Vector3(1748.93848, 2573.54199, 44.5870361), new Vector3(6.755960277, 4.69131773e-14, -90.5780869));
            API.createObject(-78626473, new Vector3(1748.95044, 2571.7981, 44.5870361), new Vector3(-0, 0, -90.6151276));
            API.createObject(962420079, new Vector3(1731.8457, 2577.01636, 49.2492256), new Vector3(0, 0, -0.529892743));
            API.createObject(962420079, new Vector3(1734.8457, 2577.01636, 49.2492256), new Vector3(0, 0, -0.529892683));
            API.createObject(962420079, new Vector3(1737.8457, 2577.01636, 49.2492256), new Vector3(0, 0, -0.529892623));
            API.createObject(962420079, new Vector3(1740.8457, 2577.01636, 49.2492256), new Vector3(0, 0, -0.529892564));
            API.createObject(962420079, new Vector3(1743.8457, 2577.01636, 49.2492256), new Vector3(0, 0, -0.529892504));
            API.createObject(962420079, new Vector3(1746.8457, 2577.01636, 49.2492256), new Vector3(0, 0, -0.529892385));
            
        }

    }
}
