var lines = File.ReadAllLines("small-input.txt");

var visibleTrees = 0;
new Forest(lines).CountVisibleTress();
