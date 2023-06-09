from urllib import request
from flask import Flask, jsonify
from SFFC  import search_finn_for_car


app = Flask(__name__)


@app.route('/search_finn_for_car/<input_value>')
def my_python_function(input_value):
    # Call your Python function with the input value and get the output values
    output_values = search_finn_for_car(input_value)
    
    # Return the output values as a JSON response
    return jsonify(output_values)


#from flask import Flask, request
#from SFFC import search_finn_for_car

#app = Flask(__name__)

#@app.route('/asearch_finn_for_car', methods=['POST'])
#def run_my_function():
    # Get the input value from the request
#    input_value = request.json['input_value']
    
    # Call the my_function function with the input value
#    output_values = asearch_finn_for_car(input_value)
    
    # Return the output values as a JSON object
#    return {'output_values': output_values}
