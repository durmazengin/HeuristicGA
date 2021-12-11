function [] = visualizeResults(node, density, isAverage)
%{
  File   : visualizeResults
  Author : Engin DURMAZ
  Date   : 30.05.2020
  Description : Procedures to read content of files belonging to
     node-density parameters and to draw graph
     Use this parameters to obtain name of result file which is obtained 
     by heuristic GA application

  Parameters: 
    -node      : number of nodes for Heuristic GA file
    -density   : density of graph for Heuristic GA file
    -isAverage : decision if average graph or best graph
%}
    resultsfolder = '../MWVCPHeuristicGA/bin/Debug';
    
    startsWith = sprintf('best_graph_%03d_0_%02d', node, density);
 
    if isAverage == 1
        startsWith = sprintf('avg_graph_%03d_0_%02d', node, density);
    end
    
    strPattern = sprintf('%s*.*', startsWith);
    files = dir(fullfile(resultsfolder, strPattern));
    figure('Name', startsWith, 'Position', [0 50 1500 700], 'NumberTitle','off');
    for k = 1: length(files)
         fileName = files(k).name;
         fullFileName = fullfile(resultsfolder, fileName);
         
         strRegex = sprintf('%s_|.txt', startsWith);
         graphTitle = regexprep(fileName, strRegex, '');
         graphTitle = strrep(graphTitle, '_',' - ')
         % disp(label);
         drawGraph(fullFileName, graphTitle, k, isAverage);
    end
    
end
function [] = drawGraph(fileNameResults, graphTitle, figureNumber, isAverage)
    % fprintf(1, 'Processing %s\n', fileNameResults);
    resultVector = dlmread(fileNameResults);
    countLines = size(resultVector, 1); 
    % disp(countLines);

    generationNumbers = 1:countLines;
            
    subplot(2, 4, figureNumber);
    plot(generationNumbers, resultVector);
    title(graphTitle);
    if isAverage == 0
        legend('Best Objectives');
    else
        legend('Average Objectives');
    end
end
        