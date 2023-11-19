package com.techelevator;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan(basePackages = "com.techelevator")
public class Application {

    public static void main(String[] args) {
        System.setProperty("CURRENT_WEEK", "7");
        SpringApplication.run(Application.class, args);
    }

}
