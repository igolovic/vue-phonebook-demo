<template>
 <b-container class="bv-example-row">

<b-row class="text-center" align="center">
   <b-button-toolbar key-nav aria-label="Toolbar with button groups">
    <b-input-group size="sm" prepend="Number of items on page:" class="mx-1">
      <b-form-select v-model="selectedPageSize" name="pageSizes" id="pageSizes" right text="Number of items on page">
        <b-form-select-option value="5">5</b-form-select-option>
        <b-form-select-option value="10">10</b-form-select-option>
      </b-form-select>
    </b-input-group>
    <b-button-group size="sm" class="mx-1">
      <b-button v-on:click="prevPage" :disabled="disablePrevBtn">Previous page</b-button>
      <b-button v-on:click="nextPage" :disabled="disableNextBtn">Next page</b-button>
    </b-button-group>
    <b-input-group size="sm" prepend="Filter:" class="mx-1">
      <b-form-input id="filterText" name="filterText" type="search" v-model="filter" value="" class="text-left"></b-form-input>
      <b-button v-on:click="filter=''">Clear</b-button>
    </b-input-group>
    <b-input-group size="sm" prepend="Application operators:" class="mx-1">
    <b-form-select v-model="selectedEmployeeWithPermissions" name="operators" id="operators" right text="Operators">
      <b-form-select-option v-for="employee in employeesWithPermissions" :key="employee.Identifier" :value="employee.Identifier">{{employee.Name}} {{employee.LastName}}</b-form-select-option>
    </b-form-select>
    </b-input-group>
   </b-button-toolbar>
</b-row>

<b-row>
      <table id="mainTable">
        <thead>
          <tr>
            <th v-for="col in columns" v-on:click="sortTable(col.property)" :key="col.property" :value="col.header">
              {{col.header}}<div class="arrow" v-if="col.property == sortColumn" v-bind:class="ascending ? 'arrow_up' : 'arrow_down'"></div>
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="employee in sortedEmployees" :key="employee.Identifier" :value="employee.Name">
            <td v-for="col in columns" :key="col.property" :value="col.header">{{employee[col.property]}}</td>
          </tr>        
        </tbody>
      </table>  
</b-row>
<b-row>
  <label v-if="hasFilteredEmployees">
    Showing page {{currentPage}} of {{showingTo}} for total of {{filteredEmployees.length}} records
  </label>
</b-row>

 </b-container>
</template>

<script>
import axios from 'axios'

export default {
  name: 'Phonebook',
  data (){
    return {
       selectedEmployeeWithPermissions:'-',
       employeesWithPermissions:[],
       employees:[],
       sortColumn: 'Name',
       ascending: true,
       filter:'',
       selectedPageSize:5,
       currentPage:1
    }
  },
  methods: {
    sortTable:function sortTable(col) {
      if (this.sortColumn === col) {
        this.ascending = !this.ascending;
      } else {
        this.ascending = true;
      }

      // Trigger computation of computed property sortedEmployees
      this.sortColumn = col;
    },
    nextPage:function nextPage() {
      if((this.currentPage * this.selectedPageSize) < this.filteredEmployees.length) this.currentPage++;
    },
    prevPage:function prevPage() {
      if(this.currentPage > 1) this.currentPage--;
    }
  },
  watch:{
    selectedEmployeeWithPermissions:function selectedEmployeeWithPermissions() {
      // Get employees visible for current operator
      axios.post('https://localhost:44354/Employees', {IdentifierUser: this.selectedEmployeeWithPermissions })
        .then(res => {
          this.employees = res.data;

          // New data has arrived - reset paging to first page
          this.currentPage = 1;
        })
    },
    filter:function filter() {
      // Filtering applied - set to first page
      this.currentPage = 1;
    },
    selectedPageSize:function selectedPageSize() {
      // Page size changed - set to first page
      this.currentPage = 1;
    }
  },
  created:
        function(){
          fetch('https://localhost:44354/EmployeesWithPermissions')
          .then(res => res.json())
          .then(res => {
            this.employeesWithPermissions = res;
          })
        },
  computed:{
    // Filter employees
    filteredEmployees:function filteredEmployees() {
      return this.employees.filter(e => {
        if (this.filter == '') 
          return true;

        return e.Name.toLowerCase().indexOf(this.filter.toLowerCase()) >= 0 || e.LastName.toLowerCase().indexOf(this.filter.toLowerCase()) >= 0;
      })
    },
    // Sort and page filtered employees
    sortedEmployees:function sortedEmployees() {
      // Sort
      return this.filteredEmployees.slice().sort((a,b) => {
        let modifier = 1;
        if(this.ascending === true) modifier = -1;
        if(a[this.sortColumn] < b[this.sortColumn]) return -1 * modifier;
        if(a[this.sortColumn] > b[this.sortColumn]) return 1 * modifier;
        return 0;
      })
      // Page
      .filter((row, index) => {
        let start = (this.currentPage - 1) * this.selectedPageSize;
        let end = start + Number(this.selectedPageSize);
        if(index >= start && index < end) return true;
      });
    },
    hasFilteredEmployees: function (){
      return this.filteredEmployees.length > 0;
    },
    disablePrevBtn: function (){
      return this.filteredEmployees.length == 0 || this.currentPage == 1;
    },
    disableNextBtn: function (){
      let difference = this.filteredEmployees.length - (Number(this.selectedPageSize) * this.currentPage);
      return this.filteredEmployees.length == 0 || difference <= 0;
    },
    showingTo: function (){
      let div = this.filteredEmployees.length / this.selectedPageSize;
      let whole = Math.floor(div);
      return whole + (((this.filteredEmployees.length % this.selectedPageSize) > 0) ? 1 : 0);
    },    
    columns:function () {
      if (this.employees.length == 0) {
        return [];
      }
      return [
        {property: 'Name', header:'Name'},
        {property: 'LastName', header:'Last name'},
        {property: 'RoleName', header:'Role name'},
        {property: 'DepartmentName', header:'Department name'},
        {property: 'MobileNumber', header:'Mobile number'},
        {property: 'Email', header:'Email'},
        {property: 'JobDetails', header:'Job details'},
        {property: 'LocationName', header:'Location name'},
      ];
    },
  }
}
</script>