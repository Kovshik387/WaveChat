export default function FooterComponent() {
  return (
    <footer style={FooterStyle} className='fixed-bottom'>
      <h1 style={{ color: 'white' }}>
        <a style={{ textDecoration: "none", color: "white", paddingLeft: "280px" }} href="https://github.com/Kovshik387/ProggramingTechnology-Authorization">GitHub</a>
      </h1>
    </footer>
  );
}

const FooterStyle: React.CSSProperties = {
  backgroundColor: "#242424",
  color: "white",
  display: "flex",
  alignItems: "center",
  marginTop: "10px",

}