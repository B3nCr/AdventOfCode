const factorial = function (num) {
  debugger;
  if (num === 0 || num === 1) {
    return 1;
  } else {
    return num * factorial(num - 1);
  }
};

console.log(factorial(5));
