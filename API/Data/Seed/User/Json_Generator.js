//https://json-generator.com/

[
  '{{repeat(12)}}',
  {
    
    Name: '{{firstName()}}',
    contact: '{{street()}} {{city()}}',
    UserName: '{{firstName()}}',
    Email: '{{email([random])}}',
    Balance: '{{doubleing([0], [100])}}',
    PhoneNumber: '{{integer([900000000], [999999999] )}}'   
  }
]
  
  