<template>
    <div>
        <input v-model="searchTerm" type="text" class="form-control custom-input" id="searchTerm" @input="fetchAutocompleteSuggestions" />
        <ul v-if="suggestions.length">
            <li v-for="suggestion in suggestions" :key="suggestion"> {{ suggestion }}</li>
        </ul>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
            searchTerm: '',
            suggestions: []
        }
    },
    methods: {
        async fetchAutocompleteSuggestions() {
            try {
                const response = await axios.get('autocomplete/quarterbacks', {
                    params: {
                        searchTerm: this.searchTerm
                    }
                });
                this.suggestions = response.data;
            } catch (error) {
                console.error('Error fetching suggestions: ', error);
            }
        }
    }
}

</script>