// Example of a typical configuration
module.exports = function (config) {
  config.set({
    basePath: "",
    frameworks: ["jasmine", "@angular-devkit/build-angular"],
    plugins: [
      require("karma-jasmine"),
      require("karma-chrome-launcher"),
      require("karma-jasmine-html-reporter"),
      require("karma-coverage-istanbul-reporter"),
      require("karma-coverage"),
      require("karma-sonarqube-reporter"),
      require("@angular-devkit/build-angular/plugins/karma"),
    ],
    client: {
      clearContext: false,
    },
    coverageIstanbulReporter: {
      reports: ["html", "lcovonly"],
      fixWebpackSourcePaths: true,
    },
    sonarqubeReporter: {
      basePath: "src/app",
      outputFolder: "reports",
      filePattern: "**/*spec.ts",
      encoding: "utf-8",
      legacyMode: false,
      reportName: (metadata) => {
        return metadata.concat("xml").join(".");
      },
    },
    angularCli: {
      environment: "dev",
    },
    reporters: ["progress", "kjhtml", "sonarqube"],
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    autoWatch: true,
    browsers: ["ChromeHeadless"],
    singleRun: false,
  });
};
