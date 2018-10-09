using UnityEngine;

/*
 * Intro & Credits!
 * C# Neuron Network library 
 * only avalaible with 3 layer neurons: 1 input layer, 1 hidden layer, and 1 output
 * 
 * Credit to Daniel Shiffman (The Cooding Train) on NN library on p5.js ( javascript base )
 * & multilayer perceptron on his youtube channel.
 * 
 * */
 
public class NeuronNetwork {
    
    Matrix wih, who, bh, bo;
    int input_node, hidden_node, output_node;

    public float learning_rate { get; set; }
    
    // initial layers (input node, hidden node, output node)
    public NeuronNetwork(int input, int hidden, int output)
    {
        input_node = input;
        hidden_node = hidden;
        output_node = output;
        learning_rate = 0.01f;
        
        wih = new Matrix(hidden_node, input_node);
        who = new Matrix(output_node, hidden_node);

        wih.Randomize();
        who.Randomize();

        bh = new Matrix(hidden_node, 1);
        bo = new Matrix(output_node, 1);

        bh.Randomize();
        bo.Randomize();
        
    }

    float sigmoid(float x)
    {
        float sigm = 1 / (1 + Mathf.Exp(-x));

        return sigm;
    }

    // gradient error = derivative of the activation function (sigmoid)
    float dsigmoid(float y)
    {
        return sigmoid(y) * (1 - sigmoid(y));
    }

    // ===>> feedforward ===>> feedforward ===>> for testing data
    public float[] predict(float[] input_array)
    {
        Matrix inputs = Matrix.fromArray(input_array);
            
        Matrix hiddens = Matrix.Multiply(wih, inputs);
        //hidden.Add(bh).Activation();
        hiddens.Add(bh).Map(sigmoid);

        Matrix outputs = Matrix.Multiply(who, hiddens);
        //outputs.Add(bo).Activation();
        outputs.Add(bo).Map(sigmoid);

        return outputs.toArray();
    }

    // <<==== backpropagation <<==== backpropagation <<==== for training data
    public void train(float[] input_array, float[] target_array)
    {
        // ################### feedforward start here ################################ 
        Matrix inputs = Matrix.fromArray(input_array);
        Matrix targetnya = Matrix.fromArray(target_array);

        Matrix hiddens = Matrix.Multiply(wih, inputs);
        //hiddens.Add(bh).Activation();
        hiddens.Add(bh).Map(sigmoid);

        // feed to the output layer / final feedforward
        Matrix outputs = Matrix.Multiply(who, hiddens);
        //outputs.Add(bo).Activation();
        outputs.Add(bo).Map(sigmoid);

        // ################### backpropagation start here ################################ 
        Matrix targets = Matrix.fromArray(target_array);
        Matrix output_errors = Matrix.Subtract(targets, outputs);

        //Matrix output_gradients = Matrix.Derivative(outputs);
        Matrix output_gradients = Matrix.Map(outputs, dsigmoid);
        // output derivative * output errors * learning rate
        output_gradients.Multiply(output_errors).Multiply(learning_rate);

        Matrix hidden_T = Matrix.Transpose(hiddens);
        Matrix delta_who = Matrix.Multiply(output_gradients, hidden_T);
        
        who.Add(delta_who);
        bo.Add(output_gradients);

        // calculate hidden errors
        Matrix who_T = Matrix.Transpose(who);
        Matrix hidden_errors = Matrix.Multiply(who_T, output_errors);

        //Matrix hidden_gradient = Matrix.Derivative(hiddens);
        Matrix hidden_gradient = Matrix.Map(hiddens, dsigmoid);
        // hidden derivative * hidden errors * learning rate
        hidden_gradient.Multiply(hidden_errors).Multiply(learning_rate);

        Matrix input_T = Matrix.Transpose(inputs);
        Matrix delta_wih = Matrix.Multiply(hidden_gradient, input_T);

        wih.Add(delta_wih);
        bh.Add(hidden_gradient);
        
    }
}
