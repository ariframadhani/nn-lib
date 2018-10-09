using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOR : MonoBehaviour {

    NeuronNetwork nn;
    Dataset data1, data2, data3, data4;
    List<Dataset> list;
    float[] inputs, targets, test;
    int trainTime = 0;

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
     * with "train" method by neuron network, it takes 2 arguments: 
     * dataset input and dataset target
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

        // counting how much time untill it get the best training result.
        trainTime += Time.time;
        
    }


    void data_test()
    {
        float[] test = new float[2] { 0, 0 };  
        float[] test2 = new float[2] { 0, 1 }; 
        float[] test3 = new float[2] { 1, 0 }; 
        float[] test4 = new float[2] { 1, 1 };

        float[] predict = nn.predict(test);
        float[] predict2 = nn.predict(test2);
        float[] predict3 = nn.predict(test3);
        float[] predict4 = nn.predict(test4);

        Debug.Log("output data test");

        Debug.Log(predict[0]); // output should be 0 ( false )
        Debug.Log(predict2[0]); // output should be 1 ( true )
        Debug.Log(predict3[0]); // output should be 1 ( true )
        Debug.Log(predict4[0]); // output should be 0 ( false )

    }


    // Update is called once per frame
    void Update ()
    {
        // run the "train" method, to train the datasets
        train();

        // testing data with output
        data_test();
    }
}
 