//https://json-generator.com/

[
  '{{repeat(12)}}',
  {
    
    name: '{{firstName()}}',
    contact: '{{street()}} {{city()}}',
    UserName: '{{firstName()}}',
    Email: '{{email([random])}}',
    PhoneNumber: '{{integer([900000000], [999999999] )}}'   
  }
]
  
  