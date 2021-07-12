<template>
 <div>
   <ul class="horizontal">
     <li>
        <label for="pageSizes" >Page size:</label>
        <select v-model="selectedPageSize" name="pageSizes" id="pageSizes">
            <option id="5">5</option>
            <option id="10">10</option>
          </select>
     </li>     
     <li>
        <button v-on:click="prevPage" :disabled="disablePrevBtn">Previous page</button> 
        <button v-on:click="nextPage" :disabled="disableNextBtn">Next</button>
     </li>     
     <li>
       <label for="filterText" >Filter:</label>
      <input id="filterText" name="filterText" type="search" v-model="filter">
     </li>
      <li>
      <button type="button" v-on:click="filter=''" >Clear</button>
     </li>
     <li>
     </li>
     <li>
        <label for="operators" >Current operator:</label>
        <select v-model="selectedEmployeeWithPermissions" name="operators" id="operators" class="form-control operator">
            <option v-for="employee in employeesWithPermissions" :key="employee.Identifier" :value="employee.Identifier">{{ employee.Name }}</option>
          </select>
     </li>
   </ul>

<div>
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
</div>

 </div>
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
      // if(this.currentPage == 1)
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
    disablePrevBtn: function (){
      return this.sortedEmployees.length == 0 || this.currentPage == 1;
    },
    disableNextBtn: function (){
      return this.sortedEmployees.length == 0 || (this.selectedPageSize * this.currentPage) > this.filteredEmployees.length;
    },    
    columns:function columns() {
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