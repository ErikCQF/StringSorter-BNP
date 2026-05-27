using BusinessLibrary;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //Arrage
            StringSorter src = new StringSorter();
            var test = new string[6];
            test[0] = "4 Exotic options | 4.6 Barrier options | Quanto options";
            test[1] = "4 Exotic options » 4.2 Rainbow options » 4.2.1 Lookback options";
            test[2] = "10 chapter >> 10.1 sub-chapter";
            test[3] = "1 chapter >> 1.1 sub-chapter >> 1.1.1 sub-sub-chapter";
            test[4] = "2 section >> 2.4 section >> 2.4.0 sect4.3ion";
            test[5] = "no numbers here at all";



            var expected = new string[6];
            expected[0] = "1 chapter >> 1.1 sub-chapter >> 1.1.1 sub-sub-chapter";
            expected[1] = "2 section >> 2.4 section >> 2.4.0 sect4.3ion";
            expected[2] = "4 Exotic options » 4.2 Rainbow options » 4.2.1 Lookback options";
            expected[3] = "4 Exotic options | 4.6 Barrier options | Quanto options";
            expected[4] = "10 chapter >> 10.1 sub-chapter";
            expected[5] = "no numbers here at all";


            //Act 

            var result = src.Sort(test);

            //Assert

            for(int i=0;i<=expected.Length;i++)
            {
                Assert.Equal(expected[i], result[i]);
            }

        }
    }
}