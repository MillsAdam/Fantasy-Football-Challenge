package com.techelevator.controller.position.quarterback;

import com.techelevator.dao.position.quarterback.QuarterbackDao;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@CrossOrigin
@RestController
public class QuarterbackController {
    private QuarterbackDao quarterbackDao;
    public QuarterbackController(QuarterbackDao quarterbackDao) {
        this.quarterbackDao = quarterbackDao;
    }

    @GetMapping("/autocomplete/quarterbacks")
    public List<String> autocompleteQuarterbacks(@RequestParam String searchTerm) {
        return quarterbackDao.getQuarterbackNames(searchTerm);
    }
}
