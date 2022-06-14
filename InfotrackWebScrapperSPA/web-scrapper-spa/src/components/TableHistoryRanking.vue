<template>
  <div class="tablehistory">
    <table-lite
      class="table-lite"
      :is-static-mode="true"
      :is-loading="table.isLoading"
      :columns="table.columns"
      :rows="table.rows"
      :total="table.totalRecordCount"
      :sortable="table.sortable"
      @is-finished="table.isLoading = false"
    ></table-lite>
  </div>
</template>

<script>
import TableLite from "vue3-table-lite";
import { defineComponent, reactive } from "vue";
import scrapperServices from "../services/scrapperServices";

export default defineComponent({
  name: "TableHistoryRanking",
  components: { TableLite },
  setup() {
    // Init Your table settings
    const table = reactive({
      isLoading: false,
      columns: [
        {
          label: "URL",
          field: "url",
          width: "3%",
          sortable: true,
          isKey: true,
        },
        {
          label: "Keywords",
          field: "keywords",
          width: "5%",
          sortable: true,
        },
        {
          label: "Ranking",
          field: "ranking",
          width: "5%",
          sortable: true,
        },
        {
          label: "Search Date",
          field: "searchDate",
          width: "5%",
          sortable: true,
        },
        {
          label: "Occurrences",
          field: "occurrences",
          width: "1%",
          sortable: true,
        },
      ],
      rows: [],
      totalRecordCount: 5,
      sortable: {
        order: "searchDate",
        sort: "asc",
      },
    });

    const doSearch = (offset, limit, order, sort) => {
      table.isLoading = true;

      scrapperServices.getScrapperHistory().then((response) => {
        table.rows = response.data;
        table.totalRecordCount = response.data.length;
        table.sortable.order = order;
        table.sortable.sort = sort;
      });
    };

    const tableLoadingFinish = (elements) => {
      table.isLoading = false;
    };

    doSearch(0, 10, "searchDate", "asc");

    return {
      table,
      doSearch,
      tableLoadingFinish,
    };
  },
});
</script>

<style>
.tablehistory {
  margin-left: 20%;
}
.table-lite {
  width: 80%;
}
</style>
