using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOR : MonoBehaviour {

    NeuronNetwork nn;
    Dataset data1, data2, data3, data4;
    List<Dataset> list;
    float[] inputs, targets, test;

    // Use this for initialization
    void Start() {
        list = new List<Dataset>();
        nn = new NeuronNetwork(2, 4, 1)
        {
            learning_rate = 0.15f
        };

        setupDataset();
        
    }

    /**
     * setup all dataset from inputs and target for XOR logic
     * then add to List
     * 
     * */
    void setupDataset()
    {
        data1 = new Dataset
        {
            inputs = new float[] { 0, 0 },
            targets = new float[] { 0 },
        };

        data2 = new Dataset
        {
            inputs = new float[] { 0, 1 },
            targets = new float[] { 1 },
        };

        data3 = new Dataset
        {
            inputs = new float[] { 1, 0 },
            targets = new float[] { 1 },
        };

        data4 = new Dataset
        {
            inputs = new float[] { 1, 1 },
            targets = new float[] { 0 },
        };

        list.Add(data1);
        list.Add(data2);
        list.Add(data3);
        list.Add(data4);
    }
    
    /**
     * train the dataset
     * with "train" method on neuron network library, it takes 2 arguments: 
     * dataset inputs and dataset targets as an array
     * 
     * */
    void train()
    {
        for (int i = 0; i < 10; i++)
        {
            foreach (var data in list)
            {
                nn.train(data.inputs, data.targets);
            }
        }

        data_test();
    }

    /**
     * test the data with XOR input
     * */

    void data_test()
    {
        float[] test1 = new float[2] { 0, 0 };  
        float[] test2 = new float[2] { 0, 1 }; 
        float[] test3 = new float[2] { 1, 0 }; 
        float[] test4 = new float[2] { 1, 1 };

        float[] predict1 = nn.predict(test1);
        float[] predict2 = nn.predict(test2);
        float[] predict3 = nn.predict(test3);
        float[] predict4 = nn.predict(test4);

        Debug.Log("output data test");

        //Debug.Log(predict1[0]); // output should be 0 ( false )
        //Debug.Log(predict2[0]);  // output should be 1 ( true )
        //Debug.Log(predict3[0]);  // output should be 1 ( true )
        //Debug.Log(predict4[0]);  // output should be 0 ( false )

        
        /**
         * NOTE: if the output data got stucked beetwen 4 and 5 for a long time, restart train data
         * 
         * cause: random weight input wasn't good enaugh to train
         * */

        // if predict output[0] is less then 0.01 / false, then done
        if (predict1[0] < 0.01)
            Debug.Log("Done");
        else
            Debug.Log(predict4[0]);



    }


    // Update is called once per frame
    void Update ()
    {
        // run the "train" method, to train the datasets
        train();
    }
}
 