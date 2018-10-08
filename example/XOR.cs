using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XOR : MonoBehaviour {

    NeuronNetwork nn;
    Dataset data, data1, data2, data3;
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

    }

    void train()
    {
        data = new Dataset
        {
            inputs = new float[] { 0, 0 },
            targets = new float[] { 0 },
        };

        data1 = new Dataset
        {
            inputs = new float[] { 0, 1 },
            targets = new float[] { 1 },
        };

        data2 = new Dataset
        {
            inputs = new float[] { 1, 0 },
            targets = new float[] { 1 },
        };

        data3 = new Dataset
        {
            inputs = new float[] { 1, 1 },
            targets = new float[] { 0 },
        };

        list.Add(data);
        list.Add(data1);
        list.Add(data2);
        list.Add(data3);

        for (int i = 0; i < 10; i++)
        {
            foreach (var data in list)
            {
                nn.train(data.inputs, data.targets);
            }

        }
        trainTime++;
        data_test();
    }


    void data_test()
    {
        float[] test = new float[2] { 0, 0 };
        float[] test2 = new float[2] { 0, 1 };
        float[] test3 = new float[2] { 1, 0 };
        float[] test4 = new float[2] { 1, 1 };

        float[] predict = nn.predict(test);
        //float[] predict2 = nn.predict(test2);
        //float[] predict3 = nn.predict(test3);
        //float[] predict4 = nn.predict(test4);

        Debug.Log("output data test");

        Debug.Log(predict[0]);
        //Debug.Log(predict2[0]);
        //Debug.Log(predict3[0]);
        //Debug.Log(predict4[0]);

    }


    // Update is called once per frame
    void Update ()
    {
        train();
        Debug.Log(trainTime);
    }
}
 